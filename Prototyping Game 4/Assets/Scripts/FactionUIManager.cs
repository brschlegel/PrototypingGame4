using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FactionUIManager : MonoBehaviour
{
    public TextMeshProUGUI factionName;
    public Transform rumorHolder;
    public GameObject rumorDisplayPrefab;
    public TextMeshProUGUI bribeText;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetFactionName(string name)
    {
        factionName.text = name;    
    }

    public void DisplayRumors(List<string> rumors)
    {
        foreach (string r in rumors)
        {
            GameObject g = Instantiate(rumorDisplayPrefab, rumorHolder);
            g.transform.GetComponentInChildren<TextMeshProUGUI>().text = r;
        }
    }

    public void DisplayRumor(string r)
    {
        GameObject g = Instantiate(rumorDisplayPrefab, rumorHolder);
        g.transform.GetComponentInChildren<TextMeshProUGUI>().text = r;
    }

    public void SetBribeCost(int cost)
    {
        bribeText.text = "RUMOR \n " + cost + " GOLD";
    }


}
