using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType { Sword, Axe, Spear}
public struct Gladiator
{
    public float strength;
    public float weaponQuality;
    public WeaponType weaponType;
    public float armourQuality;
    public float health;
    public float missChance;

    public override string ToString()
    {
        return string.Format("Strength: {0} | weaponQuality: {1} | weaponType: {2} | armourQuality: {3} | health: {4} | missChance: {5}", strength, weaponQuality, weaponType, armourQuality, health, missChance);
    }
}

public class GladiatorGenerator : MonoBehaviour
{

    public float strengthMin;
    public float strengthMax;
    public float weaponQualityMin;  
    public float weaponQualityMax;
    public float armourQualityMin;
    public float armourQualityMax;
    public float healthMin;
    public float healthMax;
    public float missChanceMin;
    public float missChanceMax;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            Debug.Log(GenerateGladiator());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Gladiator GenerateGladiator()
    {
        Gladiator g = new Gladiator();
        g.strength = Random.Range(strengthMin, strengthMax);
        g.weaponQuality = Random.Range(weaponQualityMin, weaponQualityMax);
        g.armourQuality = Random.Range(armourQualityMin, armourQualityMax);
        g.health = Random.Range(healthMin, healthMax);
        g.missChance = Random.Range(missChanceMin, missChanceMax);
        g.weaponType = (WeaponType)Random.Range(0, 3);
        return g;
    }
}
