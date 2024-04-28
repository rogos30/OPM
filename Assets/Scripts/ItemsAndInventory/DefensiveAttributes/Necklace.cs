using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necklace : DefensiveAttribute
{
    public Necklace()
    {
        Name = "Wisiorek z kochanym nauczycielem";
        Description = "Zwi�ksza obron� o 30, ale zmniejsza celno�� o 15%";
        AttackAdded = 0;
        DefenseAdded = 30;
        HealingMultiplier = 1;
        AccuracyMultiplier = 0.85f;
        Cost = 20;
        Amount = 0;
    }
}
