using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NetheriteArmor : Armor
{
    public NetheriteArmor()
    {
        Name = "Netherytowa zbroja";
        Description = "Zwiêksza obronê o 160";
        AttackAdded = 0;
        DefenseAdded = 160;
        HealingMultiplier = 1;
        AccuracyMultiplier = 1;
        Cost = 0;
        Amount = 0;
        Id = 17;
    }
}
