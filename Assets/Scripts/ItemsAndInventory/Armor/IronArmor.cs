using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronArmor : Armor
{
    public IronArmor()
    {
        Name = "�elazna zbroja";
        Description = "Zwi�ksza obron� o 80";
        AttackAdded = 0;
        DefenseAdded = 80;
        HealingMultiplier = 1;
        AccuracyMultiplier = 1;
        Cost = 110;
        Amount = 0;
    }
}
