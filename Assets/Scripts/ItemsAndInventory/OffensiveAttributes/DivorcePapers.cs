using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivorcePapers : OffensiveAttribute
{
    public DivorcePapers()
    {
        Name = "Oblana kartkówka";
        Description = "Zwiêksza atak o 240, ale zmniejsza przyjmowane leczenie o 25%";
        AttackAdded = 240;
        DefenseAdded = 00;
        HealingMultiplier = 0.75f;
        AccuracyMultiplier = 1;
        Cost = 80;
        Amount = 0;
        Id = 15;
    }
}
