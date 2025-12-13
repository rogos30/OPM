using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NetheriteSword : Weapon
{
    public NetheriteSword()
    {
        Name = "Netherytowy miecz";
        Description = "Zwiêksza atak o 400";
        AttackAdded = 400;
        DefenseAdded = 0;
        HealingMultiplier = 1;
        AccuracyMultiplier = 1;
        Cost = 0;
        Amount = 0;
        Id = 16;
    }
}
