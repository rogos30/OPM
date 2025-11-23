using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Bat : EnemyCharacter
{
    public Bat()
    {
        NominativeName = "Nietoperz";
        DativeName = "Nietoperzowi";
        AccusativeName = "Nietoperze";
        Health = MaxHealth = DefaultMaxHealth = BaseMaxHealth = 1800;
        DifficultyHealthChange = 500;
        Attack = DefaultAttack = BaseAttack = 400;
        DifficultyAttackChange = 160;
        Defense = DefaultDefense = BaseDefense = 80;
        Accuracy = DefaultAccuracy = BaseAccuracy = 0.85f;
        Turns = DefaultTurns = BaseTurns = 1;
        Speed = 250;
        MoneyDropped = 40;
        XPDropped = 1000;
        EnemyBite attack = new EnemyBite();
        skillSet.Add(attack);
        ToxicBite toxicBite = new ToxicBite();
        skillSet.Add(toxicBite);
    }
}
