using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    // Start is called before the first frame update
    Queue<string> turnReports = new Queue<string>();
    [SerializeField] private float weaponTrianglePositive;
    [SerializeField] private float weaponTriangleNegative;
    [SerializeField] private float damageModifierMin;
    [SerializeField] private float damageModifierMax;
    [SerializeField] GladiatorGenerator gen;
    void Start()
    {
        Gladiator a = gen.GenerateGladiator();
        Gladiator b = gen.GenerateGladiator();
        Debug.Log(a);
        Debug.Log(b);
        Gladiator winner = Simulate(a,b);
        PrintReports();
        Debug.Log("Winna " + winner.name);
        



    }

    public void PrintReports()
    {
        while(turnReports.Count > 0)
        { 
            Debug.Log(turnReports.Dequeue());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Gladiator Simulate(Gladiator a, Gladiator b)
    {
        turnReports.Clear();
        while (a.health > 0 && b.health > 0)
        {
            string reportA;
            int damageA = CalculateDamage(a, b);
            if(damageA <= 0)
            {
                reportA = a.name + " MISSED!";
            }
            else
            {
                reportA = a.name + " deals " + damageA + " to " + b.name;
            }    
            turnReports.Enqueue(reportA);
            string reportB;
            int damageB = CalculateDamage(b, a);
            if (damageB <= 0)
            {
                reportB = b.name + " MISSED!";
            }
            else
            {
                reportB = b.name + " deals " + damageB + " to " + a.name;
            }
            turnReports.Enqueue(reportB);
            b.health -= damageA;
            a.health -= damageB;
        }
        return a.health <= 0 ? b : a;
        
    }

    private int CalculateDamage(Gladiator attacker, Gladiator defender)
    {
        if (Random.value > attacker.missChance)
        {
            return (int)((GetDamageModifier(attacker.weaponType, defender.weaponType) * attacker.strength * 2 + (float)(attacker.weaponQuality / 2) - defender.armourQuality) * Random.Range(damageModifierMin, damageModifierMax)  );
        }
        else
        {
            return 0;
        }    
    }

    //this isnt super pretty but oh well
    private float GetDamageModifier(WeaponType attacking, WeaponType defender)
    {
        if(attacking == defender)
        {
            return 1;
        }
        switch(attacking)
        {
            case WeaponType.Sword:
                if(defender == WeaponType.Axe)
                {
                    return weaponTrianglePositive;
                }
                else
                {
                    return weaponTriangleNegative;
                }
            case WeaponType.Axe:
                if(defender == WeaponType.Spear)
                {
                    return weaponTrianglePositive;
                }
                else
                {
                    return weaponTriangleNegative;
                }
            case WeaponType.Spear:
                if(defender == WeaponType.Sword)
                {
                    return weaponTrianglePositive;
                }
                else
                {
                    return weaponTriangleNegative;
                }
                
        }

        return 1;
    }
}
