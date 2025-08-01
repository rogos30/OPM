using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondSword : Weapon
{
    public DiamondSword()
    {
        Name = "Diamentowy miecz";
        Description = "Zwiêksza atak o 400";
        AttackAdded = 400;
        DefenseAdded = 0;
        HealingMultiplier = 1;
        AccuracyMultiplier = 1;
        Cost = 170;
        Amount = 0;
        Id = 12;
    }
}
