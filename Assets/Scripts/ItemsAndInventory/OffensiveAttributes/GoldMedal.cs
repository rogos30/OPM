using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMedal : OffensiveAttribute
{
    public GoldMedal()
    {
        Name = "Z³oty medal z turnieju ping ponga";
        Description = "Zwiêksza atak o 120 oraz zwiêksza przyjmowane leczenie o 20%";
        AttackAdded = 120;
        DefenseAdded = 0;
        HealingMultiplier = 1.2f;
        AccuracyMultiplier = 1;
        Cost = 0;
        Amount = 0;
        Id = 19;
    }
}
