using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWelenc : EnemyCharacter
{
    public EnemyWelenc() : base()
    {
        NominativeName = "Welenc";
        DativeName = "Welencowi";
        AccusativeName = "Welenca";
        Health = MaxHealth = DefaultMaxHealth = 400;
        DifficultyHealthChange = 100;
        Attack = DefaultAttack = 110;
        DifficultyAttackChange = 25;
        Defense = DefaultDefense = 10;
        Accuracy = DefaultAccuracy = 0.85f;
        Turns = DefaultTurns = 1;
        Speed = 300;
        MoneyDropped = 100;
        XPDropped = 5000;
        EnemyAttack attack = new EnemyAttack();
        skillSet.Add(attack);
    }
}
