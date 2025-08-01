using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OstrysChair : DefensiveAttribute
{
    public OstrysChair()
    {
        Name = "W�tpliwej jako�ci fotel Ostrego";
        Description = "Zwi�ksza obron� o 60, ale zmniejsza celno�� o 20%";
        AttackAdded = 0;
        DefenseAdded = 60;
        HealingMultiplier = 1;
        AccuracyMultiplier = 0.8f;
        Cost = 40;
        Amount = 0;
        Id = 10;
    }
}
