using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public enum factionType {Red, Green, Blue };

    [Header("Faction Type")]
    public factionType currentFaction;

    [Header("Base Truth Stats")]
    public float baseTruthPercent = 50;
    [SerializeField] float truthMargin = 10;
    [SerializeField] int truthIncrement = 1;

    [Header("Bribe Stats")]
    public float baseBribeEffect = 10;
    [SerializeField] float baseBribeSingleRumorEffect = 20;
    [SerializeField] float bribeMargin = 5;
    [SerializeField] int bribeIncrement = 1;

    [Header("Bribe Costs")]
    public float baseBribeCost = 100;
    public float basePremiumCost = 5;
    [SerializeField] float costMargin = 50;
    [SerializeField] int costIncrement = 1;
    [SerializeField] float costScale = 1.2f;

    [Header("Trust Stats")]
    public float baseTrustEffect = 10;
    [SerializeField] float trustMargin = 5;
    [SerializeField] int trustIncrement = 1;

    [Header("Mistrust Stats")]
    [SerializeField] float baseMistrustEffect = 10;
    [SerializeField] float mistrustMargin = 5;
    [SerializeField] int mistrustIncrement = 1;

    [Header("Rumors")]
    public List<string> trueRumors = new List<string>();
    public List<string> falseRumors = new List<string>();



    void Start()
    {
        //Set initial base trust
        baseTruthPercent += adjustBaseStat(truthMargin, truthIncrement);

        //Set initial base bribe effect
        float initialBaseBribe = baseBribeEffect;
        baseBribeEffect += adjustBaseStat(bribeMargin, bribeIncrement);
        //Makes sure that the single rumor base is effected as the bribe trust effect
        baseBribeSingleRumorEffect += (baseBribeEffect - initialBaseBribe);

        //Set initial bribe
        baseBribeCost += adjustBaseStat(costMargin, costIncrement);

        //Set initial base trust effect
        baseTrustEffect += adjustBaseStat(trustMargin, trustIncrement);

        //Set initial base mistrust effect
        baseMistrustEffect += adjustBaseStat(mistrustMargin, mistrustIncrement);
    }

    //Base Stat Methods
    float adjustBaseStat(float margin, int increment)
    {
        return Random.Range((int)-increment, (int)increment + 1) * margin;        
    }

    //Stat Effect Methods
    public void TrustPlayer() => baseTruthPercent = Mathf.Clamp(baseTruthPercent + baseTrustEffect, 0, 100);
    public void TrustPlayer(float baseStat) => baseTruthPercent = Mathf.Clamp(baseTruthPercent + baseStat, 0, 100);

    public void MistrustPlayer() => baseTruthPercent = Mathf.Clamp(baseTruthPercent -= baseMistrustEffect, 0, 100);

    //Rumor Methods
    public string GetRumor(float offset = 0)
    {
        int percent = Random.Range(0, 100);
        percent += (int)offset;

        if (percent <= baseTruthPercent)
            return FindRumor(trueRumors);
        else
            return FindRumor(falseRumors);
    }

    string FindRumor(List<string> rumorList)
    {
        int index = Random.Range(0, rumorList.Count);
        string rumor = rumorList[index];
        rumorList.Remove(rumor);
        return rumor;
    }

    //Action Methods
    public string VisitFaction(FactionSystem otherFaction)
    {
        TrustPlayer();
        otherFaction.MistrustPlayer();
        return FindRumor(trueRumors);
    }

    public string BribeFaction()
    {
        //Need to grab how much money the player has
        TrustPlayer(baseBribeEffect);
        baseBribeCost *= costScale;
        return GetRumor(baseBribeSingleRumorEffect);
    }

    public string PremiumBribeFaction()
    {
        //Need to grab how much premium money the player has
        TrustPlayer(baseBribeEffect);
        basePremiumCost *= costScale;
        return FindRumor(trueRumors);
    }

    //Populate Rumors
    public void PopulateTrueRumors(string[] rumors)
    {
        trueRumors.Clear();

        foreach(string rumor in rumors)
        {
            trueRumors.Add(rumor);
        }
    }

    public void PopulateFalseRumors(string[] rumors)
    {
        falseRumors.Clear();

        foreach (string rumor in rumors)
        {
            falseRumors.Add(rumor);
        }
    }

}
