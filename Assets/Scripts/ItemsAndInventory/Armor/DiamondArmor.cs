using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondArmor : Armor
{
    public DiamondArmor()
    {
        Name = "Diamentowa zbroja";
        Description = "Zwiêksza obronê o 160";
        AttackAdded = 0;
        DefenseAdded = 160;
        HealingMultiplier = 1;
        AccuracyMultiplier = 1;
        Cost = 170;
        Amount = 0;
        Id = 13;
    }
}
