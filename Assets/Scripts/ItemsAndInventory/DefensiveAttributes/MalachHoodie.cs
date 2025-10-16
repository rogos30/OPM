using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalachHoodie : DefensiveAttribute
{
    public MalachHoodie()
    {
        Name = "Bluza szkolna";
        Description = "Zwi�ksza obron� o 15, ale zmniejsza celno�� o 10%";
        AttackAdded = 0;
        DefenseAdded = 15;
        HealingMultiplier = 1;
        AccuracyMultiplier = 0.9f;
        Cost = 10;
        Amount = 0;
        Id = 2;
    }
}
