using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator1 : EnemyCharacter
{

    public Generator1() : base()
    {
        NominativeName = "Generator wuefistów";
        DativeName = "Generatorowi wuefistów";
        AccusativeName = "Generator wuefistów";
        Health = MaxHealth = DefaultMaxHealth = 2000;
        DifficultyHealthChange = 1000;
        Attack = DefaultAttack = 150;
        DifficultyAttackChange = 50;
        Defense = DefaultDefense = 40;
        Accuracy = DefaultAccuracy = 0.90f;
        Turns = DefaultTurns = 1;
        Speed = 250;
        MoneyDropped = 100;
        XPDropped = 5000;
        EnemyAttack enemyAttack = new EnemyAttack();
        skillSet.Add(enemyAttack);
        DoublePunch doublePunch = new DoublePunch();
        skillSet.Add(doublePunch);
        skillSet.Add(doublePunch);
        PowerGeneration powerGeneration = new PowerGeneration();
        skillSet.Add(powerGeneration);
    }
}
