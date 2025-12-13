using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperToast : DefensiveAttribute
{
    public SuperToast()
    {
        Name = "Super Tost";
        Description = "Zwiêksza obronê o 60 oraz zwiêksza celnoœæ o 20%";
        AttackAdded = 0;
        DefenseAdded = 60;
        HealingMultiplier = 1;
        AccuracyMultiplier = 1.2f;
        Cost = 0;
        Amount = 0;
        Id = 18;
    }
}
