using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necklace : DefensiveAttribute
{
    public Necklace()
    {
        Name = "Wisiorek z kochanym nauczycielem";
        Description = "Zwiêksza obronê o 30, ale zmniejsza celnoœæ o 15%";
        AttackAdded = 0;
        DefenseAdded = 30;
        HealingMultiplier = 1;
        AccuracyMultiplier = 0.85f;
        Cost = 20;
        Amount = 0;
    }
}
