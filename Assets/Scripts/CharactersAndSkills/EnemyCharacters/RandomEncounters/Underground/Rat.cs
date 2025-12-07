using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : EnemyCharacter
{
    public Rat()
    {
        NominativeName = "Szczur";
        DativeName = "Szczurowi";
        AccusativeName = "Szczura";
        Health = MaxHealth = DefaultMaxHealth = BaseMaxHealth = 1600;
        DifficultyHealthChange = 400;
        Attack = DefaultAttack = BaseAttack = 600;
        DifficultyAttackChange = 240;
        Defense = DefaultDefense = BaseDefense = 90;
        Accuracy = DefaultAccuracy = BaseAccuracy = 0.95f;
        Turns = DefaultTurns = BaseTurns = 1;
        Speed = 275;
        MoneyDropped = 70;
        XPDropped = 800;
        EnemyBite attack = new EnemyBite();
        skillSet.Add(attack);
        skillSet.Add(attack);
        ToxicBite toxicBite = new ToxicBite();
        skillSet.Add(toxicBite);
    }
}
