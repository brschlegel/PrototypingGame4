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
    public float baseBribe = 20;
    [SerializeField] float bribeMargin = 5;
    [SerializeField] int bribeIncrement = 1;

    [Header("Trust Stats")]
    public float baseTrust = 10;
    [SerializeField] float trustMargin = 5;
    [SerializeField] int trustIncrement = 1;

    [Header("Mistrust Stats")]
    [SerializeField] float baseMistrust = 10;
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
        baseBribe += adjustBaseStat(bribeMargin, bribeIncrement);

        //Set initial base trust effect
        baseTrust += adjustBaseStat(trustMargin, trustIncrement);

        //Set initial base mistrust effect
        baseMistrust += adjustBaseStat(mistrustMargin, mistrustIncrement);
    }

    //Base Stat Methods
    float adjustBaseStat(float margin, int increment)
    {
        return Random.Range((int)-increment, (int)increment + 1) * margin;        
    }

    //Stat Effect Methods
    public void TrustPlayer(float baseStat) => baseTruthPercent = Mathf.Clamp(baseTruthPercent + baseStat, 0, 100);

    public void MistrustPlayer() => Mathf.Clamp(baseTruthPercent -= baseMistrust, 0, 100);

    //Rumor Methods
    public string GetRumor()
    {
        int percent = Random.Range(0, 100);

        if (percent <= baseTruthPercent)
            return FindRumor(trueRumors);
        else
            return FindRumor(falseRumors);
    }

    string FindRumor(List<string> rumorList)
    {
        int index = Random.Range(0, rumorList.Count - 1);
        string rumor = rumorList[index];
        rumorList.Remove(rumor);
        return rumor;
    }

    //Populate Rumors
    public void PopulateTrueRumors(string[] rumors)
    {
        foreach(string rumor in rumors)
        {
            trueRumors.Add(rumor);
        }
    }

    public void PopulateFalseRumors(string[] rumors)
    {
        foreach (string rumor in rumors)
        {
            falseRumors.Add(rumor);
        }
    }

}
