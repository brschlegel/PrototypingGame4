using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public struct StatMessage
{
    public float threshold;
    public string message;
}
[CreateAssetMenu(menuName = "ScriptableObjects/RumorMessages")]
public class RumorMessages : ScriptableObject 
{
    [Tooltip("Ensure that the thresholds are in ascending order")]
    public List<StatMessage> messages;  

    public StatMessage GetMessage(float value)
    {
        for(int i = 0; i < messages.Count; i++)
        {
            if(value <= messages[i].threshold)
            {
                return messages[i];
            }
        }
        return messages[0];
    }
}
