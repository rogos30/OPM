using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSword : Weapon
{
    public StoneSword()
    {
        Name = "Kamienny miecz";
        Description = "Zwiêksza atak o 100";
        AttackAdded = 100;
        DefenseAdded = 0;
        HealingMultiplier = 1;
        AccuracyMultiplier = 1;
        Cost = 50;
        Amount = 0;
    }
}
