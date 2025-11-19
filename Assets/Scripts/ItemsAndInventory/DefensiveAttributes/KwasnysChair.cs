using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KwasnysChair : DefensiveAttribute
{
    public KwasnysChair()
    {
        Name = "W¹tpliwej jakoœci fotel Kwaœnego";
        Description = "Zwiêksza obronê o 60, ale zmniejsza celnoœæ o 20%";
        AttackAdded = 0;
        DefenseAdded = 60;
        HealingMultiplier = 1;
        AccuracyMultiplier = 0.8f;
        Cost = 40;
        Amount = 0;
        Id = 10;
    }
}
