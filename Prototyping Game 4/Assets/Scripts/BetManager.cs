using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BetManager : MonoBehaviour
{
    public GameObject betButton;
    public GameObject notEnoughGoldBanner;
    public CoinUIManager coinUIManager;

    private int betAmount;
    private int tempGold;
    private int cachedGold;
    [HideInInspector]
    public string side = "none";

    public TMP_InputField input;
    public TextMeshProUGUI amountText;
    public Slider slider;
    bool sliderActivate;

    public TextMeshProUGUI leftFactionName;
    public TextMeshProUGUI rightFactionName;
    
    public int BetAmount
    {
        get { return betAmount; }
        set { betAmount = value; }
    }

    public int CurrentBetTotal
    {
        get { return int.Parse(input.text); }
    }
    public int CachedGold
    {
        set 
        { 
            cachedGold = value; 
            tempGold = cachedGold;  
        }
    }
    void Start()
    {
        betAmount = 0;
        sliderActivate = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Kinda ugly to check this in update but waddya gonna do
        Debug.Log(betAmount);

        tempGold = (cachedGold - betAmount);
        coinUIManager.SetGold(tempGold);

        bool validBet = betAmount > 0;
        bool sidePicked = side == "left" || side == "right";
        bool betAmountPicked = int.Parse(input.text) != 0;
        if(sidePicked && validBet && betAmountPicked && !betButton.activeSelf)
        {
            betButton.SetActive(true);
        }
        else if( betButton.activeSelf && (!validBet||!sidePicked))
        {
            betButton.SetActive(false);
        }
    }

    public void OnEnable()
    {
        notEnoughGoldBanner.SetActive(false);   
    }

    public void Add()
    {
        int result = int.Parse(amountText.text) + betAmount;
        if (tempGold - betAmount >= 0)
        {
            Debug.Log("temp gold " + tempGold + " betamount " + betAmount);
            amountText.text = result.ToString();
            tempGold -= betAmount;
        }
        else
        {
            StartCoroutine(FlashNotEnoughGoldBanner());
        }
        coinUIManager.SetGold(tempGold);
    }

    public void Subtract()
    {
        int result = int.Parse(amountText.text) - betAmount;
        result = Mathf.Clamp(result, 0, int.MaxValue);
        tempGold = Mathf.Clamp(tempGold + betAmount, 0, cachedGold);
        amountText.text = result.ToString();    
        coinUIManager.SetGold(tempGold);    
    }

    public IEnumerator FlashNotEnoughGoldBanner()
    {
        notEnoughGoldBanner.SetActive(true);
        yield return new WaitForSeconds(.5f);
        notEnoughGoldBanner.SetActive(false);
    }

    public void Cancel()
    {
        coinUIManager.SetGold(cachedGold);
    }

    public void PickFaction(string side)
    {
        this.side = side;
    }

    public void SetFactionNames(string left, string right)
    {
        leftFactionName.text = left;
        rightFactionName.text = right;
    }

    public void SetCacheAmount()
    {
        if (int.TryParse(input.text, out int result))
        {
            betAmount = result;
            slider.value = ((float)result / (float)cachedGold);
        }          
    }

    public void InputSlider()
    {
        int sliderEqualivant = (int)(slider.value * cachedGold);
        input.text = sliderEqualivant.ToString();
        betAmount = sliderEqualivant;
    }

    public void ResetBet()
    {
        input.text = "0";
        slider.value = 0.0f;
        betAmount = 0;

    }
}
