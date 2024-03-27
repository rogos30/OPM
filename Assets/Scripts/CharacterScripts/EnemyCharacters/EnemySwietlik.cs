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
        Health = MaxHealth = DefaultMaxHealth = 400;
        DifficultyHealthChange = 125;
        Attack = DefaultAttack = 60;
        DifficultyAttackChange = 25;
        Defense = DefaultDefense = 25;
        Accuracy = DefaultAccuracy = 0.9f;
        Turns = DefaultTurns = 1;
        Speed = 500;
        MoneyDropped = 30;
        XPDropped = 500;
        EnemyAttack attack = new EnemyAttack();
        skillSet.Add(attack);
    }
}
