using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Money Variables")]
    public int coins;
    public int premiumCoins;
    int currentBet;

    [Header("Faction Variables")]
    public FactionSystem[] factions;

    //Factions in play
    [SerializeField] FactionSystem leftFaction;
    [SerializeField] FactionSystem rightFaction;
    [SerializeField] int numOfRumors;

    private Gladiator selectedGladiator;
    private Gladiator winner;

    [Header("Components")]
    [SerializeField] GladiatorGenerator gladGen;
    [SerializeField] Battle battleSim;

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

        //Make a copy of the faction array
        List<FactionSystem> currentFactions = new List<FactionSystem>();
        foreach(FactionSystem faction in factions)
        {
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

        //Clear faction rumors
        leftFaction.ClearRumors();
        rightFaction.ClearRumors();

        //Get initial rumors
        leftFaction.GetRumor(numOfRumors);
        rightFaction.GetRumor(numOfRumors);

        Debug.Log(leftFaction.gladiator);
        Debug.Log(rightFaction.gladiator);
    }

    void ResolveRound()
    {
        winner = battleSim.Simulate(leftFaction.gladiator, rightFaction.gladiator);

        if (winner.Equals(selectedGladiator))
            coins += (currentBet * 2);

        //Start game loop over again
        AssignFactionsAndGladiators();

    }

    void SetBet(int bet, string side)
    {
        currentBet = bet;
        coins -= bet;

        selectedGladiator = GetSide(side).gladiator;
    }

    void Visit(string side, string opponent)
    {
        GetSide(side).VisitFaction(GetSide(opponent));
    }

    void Bribe(string side)
    {
        FactionSystem currentFaction = GetSide(side);

        if (coins >= currentFaction.baseBribeCost)
        {
            currentFaction.BribeFaction();
            coins -= currentFaction.baseBribeCost;
        }
            
    }

    void BribePremium(string side)
    {
        FactionSystem currentFaction = GetSide(side);

        if (premiumCoins >= currentFaction.basePremiumCost)
        {
            currentFaction.PremiumBribeFaction();
            premiumCoins -= currentFaction.basePremiumCost;
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
}
