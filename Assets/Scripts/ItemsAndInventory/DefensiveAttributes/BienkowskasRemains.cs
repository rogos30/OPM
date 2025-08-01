using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BienkowskasRemains : DefensiveAttribute
{
    public BienkowskasRemains()
    {
        Name = "Wpó³ zgni³e szcz¹tki Bieñskowskiej";
        Description = "Zwiêksza obronê o 120, ale zmniejsza celnoœæ o 25%";
        AttackAdded = 0;
        DefenseAdded = 120;
        HealingMultiplier = 1;
        AccuracyMultiplier = 0.75f;
        Cost = 80;
        Amount = 0;
        Id = 14;
    }
}
