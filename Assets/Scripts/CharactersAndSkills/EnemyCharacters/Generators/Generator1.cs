using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator1 : EnemyCharacter
{
    public Generator1() : base()
    {
        NominativeName = "Generator wuefist�w";
        DativeName = "Generatorowi wuefist�w";
        AccusativeName = "Generator wuefist�w";
        Health = MaxHealth = DefaultMaxHealth = BaseMaxHealth = 2000;
        DifficultyHealthChange = 1000;
        Attack = DefaultAttack = BaseAttack = 150;
        DifficultyAttackChange = 50;
        Defense = DefaultDefense = BaseDefense = 40;
        Accuracy = DefaultAccuracy = BaseAccuracy = 0.90f;
        Turns = DefaultTurns = BaseTurns = 1;
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
