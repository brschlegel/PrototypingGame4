using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleUIManager : MonoBehaviour
{
    public Transform turnReportHolder;
    public GameObject displayPrefab;
    public float timeBetweenReports;
    public TextMeshProUGUI resultsText;
    public GladiatorStatPopulator leftStats;
    public GladiatorStatPopulator rightStats;
    public GameObject continueButton;

    private Queue<string> reports;
    private Gladiator winner;
    private Gladiator predictedWinner;

    public Gladiator Winner
    {
        set { winner = value; }
    }
    public Gladiator PredictedWinner
    {
        set { predictedWinner = value; }
    }
    
    public Queue<string> Reports
    {
        set { reports = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ShowBattle()
    {
        while(reports.Count > 0)
        {
            yield return new WaitForSeconds(timeBetweenReports);
            GameObject g = Instantiate(displayPrefab, turnReportHolder);
            g.GetComponentInChildren<TextMeshProUGUI>().text = reports.Dequeue();
        }
        GameObject final = Instantiate(displayPrefab, turnReportHolder);
        final.GetComponentInChildren<TextMeshProUGUI>().text = winner.name + " WINS!";
        
        if(winner.Equals( predictedWinner))
        {
            resultsText.color = Color.green;
            resultsText.text = "YOU WON!";
        }
        else
        {
            resultsText.color = Color.red;
            resultsText.text = "YOU LOST";
        }
        resultsText.gameObject.SetActive(true);
        continueButton.SetActive(true);
    }

    public void BattleButton()
    {
        StartCoroutine(ShowBattle());   
    }

    public void OnDisable()
    {
        foreach(Transform child in turnReportHolder)
        {
            Destroy(child.gameObject);
        }
        resultsText.gameObject.SetActive(false);
        continueButton.SetActive(false);
    }

    public void SetStats(Gladiator left, Gladiator right)
    {
        leftStats.DisplayStats(left);
        rightStats.DisplayStats(right);
    }
}
