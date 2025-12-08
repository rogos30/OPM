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
        Health = MaxHealth = DefaultMaxHealth = BaseMaxHealth = 4000;
        DifficultyHealthChange = 2000;
        Attack = DefaultAttack = BaseAttack = 175;
        DifficultyAttackChange = 75;
        Defense = DefaultDefense = BaseDefense = 60;
        Accuracy = DefaultAccuracy = BaseAccuracy = 0.90f;
        Turns = DefaultTurns = BaseTurns = 1;
        Speed = 350;
        MoneyDropped = 100;
        XPDropped = 4000;
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
