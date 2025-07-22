using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator2 : EnemyCharacter
{
    public Generator2() : base()
    {
        NominativeName = "Generator w ³azience";
        DativeName = "Generatorowi w ³azience";
        AccusativeName = "Generator w ³azience";
        Health = MaxHealth = DefaultMaxHealth = 11500;
        DifficultyHealthChange = 2500;
        Attack = DefaultAttack = 125;
        DifficultyAttackChange = 30;
        Defense = DefaultDefense = 60;
        Accuracy = DefaultAccuracy = 0.90f;
        Turns = DefaultTurns = 1;
        Speed = 350;
        MoneyDropped = 500;
        XPDropped = 5000;
        EnemyAttack enemyAttack = new EnemyAttack();
        skillSet.Add(enemyAttack);
        skillSet.Add(enemyAttack);
        DoublePunch doublePunch = new DoublePunch();
        skillSet.Add(doublePunch);
        skillSet.Add(doublePunch);
        PowerGeneration powerGeneration = new PowerGeneration();
        skillSet.Add(powerGeneration);
        Shock shock = new Shock();
        skillSet.Add(shock);
        skillSet.Add(shock);
    }
}
