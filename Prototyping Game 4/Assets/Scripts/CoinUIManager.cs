using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI gemText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGold(int num)
    {
        goldText.text = num + " GOLD";
    }

    public void SetGems(int num)
    {
        gemText.text = num + " GEMS";
    }
}
