using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType { Sword, Axe, Spear}
public struct Gladiator
{
    public int strength;
    public int weaponQuality;
    public WeaponType weaponType;
    public int armourQuality;
    public int health;
    public float missChance;
    public string name;

    public override string ToString()
    {
        return string.Format("Name: {0} | Strength: {1} | weaponQuality: {2} | weaponType: {3} | armourQuality: {4} | health: {5} | missChance: {6:0.00}", name,strength, weaponQuality, weaponType, armourQuality, health, missChance);
    }
}

public class GladiatorGenerator : MonoBehaviour
{

    public int strengthMin;
    public int strengthMax;
    public int weaponQualityMin;  
    public int weaponQualityMax;
    public int armourQualityMin;
    public int armourQualityMax;
    public int healthMin;
    public int healthMax;
    public float missChanceMin;
    public float missChanceMax;
    //Names of roman emperors from https://www.britannica.com/topic/list-of-Roman-emperors-2043294
    private List<string> names = new List<string>() { "Augustus", "Tiberius", "Caligula", "Cladius", "Nero", "Galba", "Otho", "Aulus Vitellius", "Vespasian", "Titus", "Domitian", "Nerva", "Trajan", "Hadrian", "Anoninus Pius", "Marcus Aurelius", "Lucius Verus", "Commodus", "Publius Helvius Pertinax", "Marcus Didisu Severus Julianus", "Septimius Severus", "Caracalla", "Publius Septimius Geta", "Macrinus", "Elagabalus", "Severus Alexander", "Maximinus", "Gordian I", "Gordian II", "Pupienus Maximus", "Balbinus", "Gordian III", "Philip", "Decius", "Hostilian", "Gallus", "Aemilian", "Valerian", "Gallienus", "Claudius II Gothicus", "Quintillus", "Aurelian", "Tacitus", "Florian", "Probus", "Carus", "Numerian", "Carinus", "Diocletian", "Maximian", "Constantius I", "Galerius", "Severus", "Maxentius", "Constantine I", "Galerius Valerius Maximinus", "Licinius", "Constantine II", "Constantius II", "Constans I", "Gallus Caesar", "Julian", "Jovian", "Valentinian I", "Valens", "Gratian", "Valentinian II", "Theodosius I", "Arcadius", "Magnus Maximus", "Honorius", "Theodosius II", "Constantius III", "Valentinian III", "Marcian", "Petronius Maximus", "Avitus", "Majorian", "Libius Severus", "Anthemius", "Olybrius", "Glycerius", "Julius Nepos", "Romulus Augustulus", "Leo I", "Leo II", "Zeno" };
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Gladiator GenerateGladiator()
    {
        Gladiator g = new Gladiator();
        g.name = names[Random.Range(0, names.Count)];
        g.strength = Random.Range(strengthMin, strengthMax);
        g.weaponQuality = Random.Range(weaponQualityMin, weaponQualityMax);
        g.armourQuality = Random.Range(armourQualityMin, armourQualityMax);
        g.health = Random.Range(healthMin, healthMax);
        g.missChance = Random.Range(missChanceMin, missChanceMax);
        g.weaponType = (WeaponType)Random.Range(0, 3);
        return g;
    }
}
