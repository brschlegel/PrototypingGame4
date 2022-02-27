using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public enum factionType {Red, Green, Blue };

    [Header("Faction Type")]
    public factionType currentFaction;

    [Header("Gladiator")]
    public Gladiator gladiator;

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
    public int baseBribeCost = 100;
    public int basePremiumCost = 5;
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
    [SerializeField]RumorGenerator rumorGen;
    public List<string> rumors = new List<string>();



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
        baseBribeCost += (int)adjustBaseStat(costMargin, costIncrement);

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
    public List<string> GetRumor(int numOfRumors, float offset = 0)
    {
        float percent = baseTruthPercent;
        percent += (int)offset;

        List<string> newRumors = rumorGen.GetRumorsAboutGladiator(gladiator, numOfRumors, (percent / 100.0f));
        foreach(string rumor in newRumors)
        {
            rumors.Add(rumor);
        }
        return newRumors;
    }

    string GetTrueRumor(int numOfRumors)
    {
        List<string> newRumors = rumorGen.GetRumorsAboutGladiator(gladiator, numOfRumors, 1.0f);
        foreach (string rumor in newRumors)
        {
            rumors.Add(rumor);
        }
        return newRumors[0];
    }

    //Action Methods
    public string VisitFaction(FactionSystem otherFaction)
    {
        TrustPlayer();
        otherFaction.MistrustPlayer();
        return GetTrueRumor(1);
    }

    public string BribeFaction()
    {
        TrustPlayer(baseBribeEffect);
        baseBribeCost = (int)((float)baseBribeCost * costScale);
        return GetRumor(1, baseBribeSingleRumorEffect)[0];
    }

    public string PremiumBribeFaction()
    {
        TrustPlayer(baseBribeEffect);
        basePremiumCost = (int)((float)basePremiumCost * costScale);
        return GetTrueRumor(1);
    }

    public void ClearRumors() => rumors.Clear();
}
