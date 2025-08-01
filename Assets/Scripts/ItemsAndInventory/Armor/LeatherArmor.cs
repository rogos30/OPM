using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeatherArmor : Armor
{
    public LeatherArmor()
    {
        Name = "Skórzana zbroja";
        Description = "Zwiêksza obronê o 20";
        AttackAdded = 0;
        DefenseAdded = 20;
        HealingMultiplier = 1;
        AccuracyMultiplier = 1;
        Cost = 20;
        Amount = 0;
        Id = 1;
    }
}
