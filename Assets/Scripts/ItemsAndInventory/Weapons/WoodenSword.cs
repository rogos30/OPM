using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenSword : Weapon
{
    public WoodenSword()
    {
        Name = "Drewniany miecz";
        Description = "Zwiêksza atak o 50";
        AttackAdded = 50;
        DefenseAdded = 0;
        HealingMultiplier = 1;
        AccuracyMultiplier = 1;
        Cost = 20;
        Amount = 0;
    }
}
