using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Instructions : MonoBehaviour
{
    public TextMeshProUGUI insText;

    void Start()
    {
        insText.text =
            "1.\tThe goal is to <b>gain as much gold coins</b> as you can through a series of rounds by <b>betting</b> on a\n\tgladiator that would win a duel.\n" +
            "2.\tEach gladiator's stats (health, strength, accuracy, armor..) are different and they are also equipped\n\twith a sword, axe, or spear." +
            " Everything about the gladiator will affect the chances of winning the match.\n" +
            "3.\t<b>Gain information</b> through rumors from both teams. But be careful! rumors may not always be true.\n" +
            "4.\t<b>Paying</b> a team with <color=yellow>gold</color> will reveal an additional rumor and with <color=blue>gem</color> will reveal a fact about\n\tthat team.\n" +
            "5.\t<b>Visiting</b> a team will also reveal a fact about that team but you will be risking making the other team\n\tunhappy that can have an" +
            " effect on costs and trueness of a runor.\n" +
            "6.\tAfter studying both gladiators, <b>pick a side</b>, and <b>choose the amount of coins</b> you want to bet!\n" +
            "7.\tIf your gladiator wins, you earn double the coins you bet."; 
    }

    public void LoadBackScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }


}
