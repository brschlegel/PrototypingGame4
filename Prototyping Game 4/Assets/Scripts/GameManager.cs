using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Money Variables")]
    [SerializeField]
    private int coins;
    public int premiumCoins;
    int currentBet;

    public int Coins
    {
        get { return coins; }
        set
        {
            coins = value;
            coinUI.SetGold(value);
        }
    }

    public int PremiumCoins
    {
        get { return premiumCoins; } 
        set
        {
            premiumCoins = value;
            coinUI.SetGems(value);  
        }
    }
    [Header("Faction Variables")]
    public FactionSystem[] factions;

    //Factions in play
    [SerializeField] FactionSystem leftFaction;
    [SerializeField] FactionSystem rightFaction;
    [SerializeField] int numOfRumors;

    [SerializeField] FactionUIManager leftUI;
    [SerializeField] FactionUIManager rightUI;

    private Gladiator selectedGladiator;
    private Gladiator winner;

    [Header("Components")]
    [SerializeField] GladiatorGenerator gladGen;
    [SerializeField] Battle battleSim;
    [SerializeField] RumorGenerator rumorGen;
    [SerializeField] CoinUIManager coinUI;
    [SerializeField] BetManager betManager;
    [SerializeField] BattleUIManager battleUI;

    private bool visited = false;
    // Start is called before the first frame update
    void Start()
    {
        AssignFactionsAndGladiators();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Randomly assign the factions participating for the round
    /// </summary>
    void AssignFactionsAndGladiators()
    {
        //Clear out the factions from the previous round
        leftFaction = null;
        rightFaction = null;
        rumorGen.Reset();
        visited = false;

        //Make a copy of the faction array
        List<FactionSystem> currentFactions = new List<FactionSystem>();
        foreach(FactionSystem faction in factions)
        {
            faction.ResetCosts();
            currentFactions.Add(faction);
        }

        //Assign factions and ensures the same faction isn't chosen twice
        leftFaction = currentFactions[Random.Range(0, currentFactions.Count)];
        currentFactions.Remove(leftFaction);

        rightFaction = currentFactions[Random.Range(0, currentFactions.Count)];
        currentFactions.Remove(rightFaction);

        //Setup the gladiators
        leftFaction.gladiator = gladGen.GenerateGladiator();
        rightFaction.gladiator = gladGen.GenerateGladiator();


        
        //Setup UI
        leftUI.SetFactionName(leftFaction.name);
        rightUI.SetFactionName(rightFaction.name);

        leftFaction.ClearRumors();
        rightFaction.ClearRumors();
        leftUI.Reset();
        rightUI.Reset();

        leftFaction.GetRumor(numOfRumors);
        rightFaction.GetRumor(numOfRumors);

        leftUI.DisplayRumors(leftFaction.rumors);
        rightUI.DisplayRumors(rightFaction.rumors);

        leftUI.SetBribeCost((int)leftFaction.currentCost);
        rightUI.SetBribeCost((int)rightFaction.currentCost);

        coinUI.SetGems(PremiumCoins);
        coinUI.SetGold(Coins);

        Debug.Log(leftFaction.gladiator);
        Debug.Log(rightFaction.gladiator);
    }

    Gladiator ResolveRound()
    {
        winner = battleSim.Simulate(leftFaction.gladiator, rightFaction.gladiator);

        if (winner.Equals(selectedGladiator))
            coins += (currentBet * 2);

        //Start game loop over again
        AssignFactionsAndGladiators();
        return winner;
    }

    void SetBet(int bet, string side)
    {
        currentBet = bet;
        Coins -= bet;

        selectedGladiator = GetSide(side).gladiator;
    }

    public void Visit(string side)
    {
        if (!visited)
        {
            string opponent = side == "left" ? "right" : "left";
            string r = GetSide(side).VisitFaction(GetSide(opponent));
            if (side == "left")
            {
                leftUI.DisplayRumor(r);
            }
            else
            {
                rightUI.DisplayRumor(r);
            }
            visited = true;
        }
    }

    public void Bribe(string side)
    {
        FactionSystem currentFaction = GetSide(side);

        if (coins >= currentFaction.baseBribeCost)
        {
            Coins -= currentFaction.currentCost;
            string r = currentFaction.BribeFaction();
       
            if(side == "left")
            {
                leftUI.DisplayRumor(r);
                leftUI.SetBribeCost(leftFaction.currentCost);
            }
            else
            {
                rightUI.DisplayRumor(r);
                rightUI.SetBribeCost(rightFaction.currentCost);
            }
        }
            
    }

    public void BribePremium(string side)
    {
        FactionSystem currentFaction = GetSide(side);

        if (premiumCoins >= currentFaction.basePremiumCost)
        {
            string r = currentFaction.PremiumBribeFaction();
            PremiumCoins -= currentFaction.basePremiumCost;
            if (side == "left")
            {
                leftUI.DisplayRumor(r);
            }
            else
            {
                rightUI.DisplayRumor(r);
            }
        }
     
    }

    //Helper Function
    FactionSystem GetSide(string side)
    {
        if (side == "left")
            return leftFaction;
        else if (side == "right")
            return rightFaction;

        return null;
    }

    public void ActivateBetScreen()
    {
        betManager.CachedGold = Coins;
        betManager.SetFactionNames(leftFaction.name, rightFaction.name);
        betManager.gameObject.SetActive(true);
    }

    public void BetButton()
    {
        battleUI.SetStats(leftFaction.gladiator, rightFaction.gladiator);
        SetBet(betManager.CurrentBetTotal, betManager.side);
        battleUI.Winner = ResolveRound();
        battleUI.Reports = battleSim.turnReports;
        battleUI.PredictedWinner = selectedGladiator;
      
        

    }

}
