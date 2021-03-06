using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FactionUIManager : MonoBehaviour
{
    public TextMeshProUGUI factionName;
    public Image factionSprite;
    public Transform rumorHolder;
    public GameObject rumorDisplayPrefab;
    public TextMeshProUGUI bribeText;
    public TextMeshProUGUI trustText;

    public List<GameObject> rumorGenButtons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Super ugly, but need a way to stop the player from getting more rumors than there are stats
        if(rumorHolder.childCount >=6)
        {
            foreach(GameObject g in rumorGenButtons)
            {
                g.SetActive(false); 
            }
        }
        else
        {
            foreach (GameObject g in rumorGenButtons)
            {
                g.SetActive(true);
            }
        }
    }
    public void SetFactionName(string name)
    {
        factionName.text = name;    
    }

    public void SetFactionSprite(Sprite sprite)
    {
        factionSprite.sprite = sprite;
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

    public void DisplayFact(string r)
    {
        GameObject g = Instantiate(rumorDisplayPrefab, rumorHolder);
        TextMeshProUGUI fact = g.transform.GetComponentInChildren<TextMeshProUGUI>();
        fact.text = r;
        fact.color = Color.green;
    }


    public void ClearRumors()
    {
        foreach(Transform t in rumorHolder)
        {
            Destroy(t.gameObject);
        }
    }
    public void SetBribeCost(int cost)
    {
        bribeText.text = "RUMOR \n " + cost + " GOLD";
    }

    public void UpdateTrust(int percent)
    {
        if (percent < 30)
            trustText.color = Color.red;
        else if (percent >= 30 && percent <= 70)
            trustText.color = Color.yellow;
        else if (percent > 70)
            trustText.color = Color.green;

        trustText.text = percent.ToString() + "%";
    }

    public void Reset()
    {
        ClearRumors();
        foreach (GameObject g in rumorGenButtons)
        {
            g.SetActive(true);
        }
    }


}
