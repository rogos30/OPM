using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainmailArmor : Armor
{
    public ChainmailArmor()
    {
        Name = "Kolczuga";
        Description = "Zwiêksza obronê o 40";
        AttackAdded = 0;
        DefenseAdded = 40;
        HealingMultiplier = 1;
        AccuracyMultiplier = 1;
        Cost = 50;
        Amount = 0;
    }
}
