using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronSword : Weapon
{
    public IronSword()
    {
        Name = "¯elazny miecz";
        Description = "Zwiêksza atak o 200";
        AttackAdded = 200;
        DefenseAdded = 0;
        HealingMultiplier = 1;
        AccuracyMultiplier = 1;
        Cost = 110;
        Amount = 0;
    }
}
