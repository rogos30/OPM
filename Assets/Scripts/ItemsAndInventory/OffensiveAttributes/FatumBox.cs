using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatumBox : OffensiveAttribute
{
    public FatumBox()
    {
        Name = "Szerz�ce strach pude�ko fatum";
        Description = "Zwi�ksza atak o 30, ale zmniejsza przyjmowane leczenie o 10%";
        AttackAdded = 30;
        DefenseAdded = 00;
        HealingMultiplier = 0.9f;
        AccuracyMultiplier = 1;
        Cost = 10;
        Amount = 0;
        Id = 13;
    }
}
