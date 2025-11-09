using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisTable : OffensiveAttribute
{
    public TennisTable()
    {
        Name = "Stó³ do ping ponga";
        Description = "Zwiêksza atak o 120, ale zmniejsza przyjmowane leczenie o 20%";
        AttackAdded = 120;
        DefenseAdded = 00;
        HealingMultiplier = 0.80f;
        AccuracyMultiplier = 1;
        Cost = 40;
        Amount = 0;
        Id = 11;
    }
}
