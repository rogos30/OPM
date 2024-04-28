using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Welenc : EnemyCharacter
{
    public Welenc() : base()
    {
        NominativeName = "Welenc";
        DativeName = "Welencowi";
        AccusativeName = "Welenca";
        Health = MaxHealth = DefaultMaxHealth = 800;
        DifficultyHealthChange = 200;
        Attack = DefaultAttack = 110;
        DifficultyAttackChange = 25;
        Defense = DefaultDefense = 10;
        Accuracy = DefaultAccuracy = 0.85f;
        Turns = DefaultTurns = 1;
        Speed = 300;
        MoneyDropped = 50;
        XPDropped = 1000;
        EnemyAttack attack = new EnemyAttack();
        skillSet.Add(attack);
        skillSet.Add(attack);
        skillSet.Add(attack);
        DoublePunch doublePunch = new DoublePunch();
        skillSet.Add(doublePunch);
    }
}
