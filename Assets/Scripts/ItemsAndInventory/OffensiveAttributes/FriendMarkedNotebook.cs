using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendMarkedNotebook : OffensiveAttribute
{
    public FriendMarkedNotebook()
    {
        Name = "Zeszyt z podpisem kolegi";
        Description = "Zwiêksza atak o 60, ale zmniejsza przyjmowane leczenie o 15%";
        AttackAdded = 60;
        DefenseAdded = 00;
        HealingMultiplier = 0.85f;
        AccuracyMultiplier = 1;
        Cost = 20;
        Amount = 0;
        Id = 7;
    }
}
