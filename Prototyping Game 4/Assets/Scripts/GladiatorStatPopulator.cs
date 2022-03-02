using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GladiatorStatPopulator : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameLabel;
    [SerializeField] TextMeshProUGUI healthLabel;
    [SerializeField] TextMeshProUGUI strengthLabel;
    [SerializeField] TextMeshProUGUI weaponTypeLabel;
    [SerializeField] TextMeshProUGUI weaponQualityLabel;
    [SerializeField] TextMeshProUGUI armourQualityLabel;
    [SerializeField] TextMeshProUGUI missChanceLabel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayStats(Gladiator g)
    {
        nameLabel.text = g.name;
        healthLabel.text = "Health: " + g.health;
        strengthLabel.text = "Strength: " + g.strength;
        weaponTypeLabel.text = "Weapon Type: " + g.weaponType.ToString();
        weaponQualityLabel.text = "Weapon Quality: " + g.weaponQuality;
        armourQualityLabel.text = "Armour Quality: " + g.armourQuality;
        missChanceLabel.text = "Miss Chance: " + Mathf.Round(g.missChance * 1000) /1000;
    }
}
