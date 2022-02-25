using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RumorGenerator : MonoBehaviour
{
    static List<string> stats = new List<string>() {"Strength", "ArmourQuality", "WeaponQuality", "WeaponType", "Health", "MissChance" };
    public Dictionary<string, RumorMessages> messages;

    [SerializeField]
    private RumorMessages strength;
    [SerializeField]
    private RumorMessages armourQuality;
    [SerializeField]
    private RumorMessages weaponQuality;
    [SerializeField]
    private RumorMessages weaponType;
    [SerializeField]
    private RumorMessages health;
    [SerializeField]
    private RumorMessages missChance;
    // Start is called before the first frame update
    void Start()
    {
        messages = new Dictionary<string, RumorMessages>();
        messages["Strength"] = strength;
        messages["ArmourQuality"] = armourQuality;
        messages["WeaponQuality"] = weaponQuality;
        messages["WeaponType"] = weaponType;
        messages["Health"] = health;
        messages["MissChance"] = missChance;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<string> GetRumorsAboutGladiator(Gladiator g, int amount, float truthChance)
    {
        //Return string
        List<string> ret = new List<string>();

        //StatName string
        List<string> statCopy = new List<string>(stats);
        for(int i = 0; i < amount; i++)
        {
            bool truthfulness = Random.value < truthChance;
            string statName = statCopy[Random.Range(0, statCopy.Count)];
            statCopy.Remove(statName);
            switch (statName)
            {
                case "Strength":
                    ret.Add(GenerateRumor(g.strength, statName, truthfulness));
                    break;
                case "ArmourQuality":
                    ret.Add(GenerateRumor(g.armourQuality, statName, truthfulness));
                    break;
                case "WeaponQuality":
                    ret.Add(GenerateRumor(g.weaponQuality, statName, truthfulness));
                    break;
                case "Health":
                    ret.Add(GenerateRumor(g.health, statName, truthfulness));
                    break;
                case "WeaponType":
                    ret.Add(GenerateRumor((int)g.weaponType, statName, truthfulness));
                    break;
                case "MissChance":
                    ret.Add(GenerateRumor(g.missChance, statName, truthfulness));
                    break;
            }


        }
        return ret;
    }

    public string GenerateRumor(float statValue, string statName, bool truthful)
    {
        RumorMessages r = messages[statName];
        StatMessage trueMessage = r.GetMessage(statValue);
        if(truthful)
        {
            return trueMessage.message;
        }
        else
        {
            //Make a copy so we aren't editting the original list
            List<StatMessage> copy = new List<StatMessage>(r.messages);
            copy.Remove(trueMessage);
            return copy[Random.Range(0, copy.Count)].message;
        }
    }
}
