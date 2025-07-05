using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwietlik : EnemyCharacter
{

    public EnemySwietlik() : base()
    {
        NominativeName = "Œwietlik";
        DativeName = "Œwietlikowi";
        AccusativeName = "Œwietlika";
        Health = MaxHealth = DefaultMaxHealth = 1000;
        DifficultyHealthChange = 300;
        Attack = DefaultAttack = 90;
        DifficultyAttackChange = 40;
        Defense = DefaultDefense = 45;
        Accuracy = DefaultAccuracy = 0.9f;
        Turns = DefaultTurns = 1;
        Speed = 400;
        MoneyDropped = 10000;
        XPDropped = 10000;
        EnemyAttack attack = new EnemyAttack();
        skillSet.Add(attack);
        skillSet.Add(attack);
        skillSet.Add(attack);
        DoublePunch doublePunch = new DoublePunch();
        skillSet.Add(doublePunch);
    }
}
