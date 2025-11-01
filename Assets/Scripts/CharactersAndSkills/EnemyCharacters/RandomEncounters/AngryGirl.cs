using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryGirl : EnemyCharacter
{
    public AngryGirl()
    {
        NominativeName = "Wkurzona laska";
        DativeName = "Wkurzonej lasce";
        AccusativeName = "Wkurzonej laski";
        Health = MaxHealth = DefaultMaxHealth = BaseMaxHealth = 200;
        DifficultyHealthChange = 50;
        Attack = DefaultAttack = BaseAttack = 75;
        DifficultyAttackChange = 30;
        Defense = DefaultDefense = BaseDefense = 15;
        Accuracy = DefaultAccuracy = BaseAccuracy = 0.95f;
        Turns = DefaultTurns = BaseTurns = 1;
        Speed = 275;
        MoneyDropped = 20;
        XPDropped = 200;
        EnemyAttack attack = new EnemyAttack();
        skillSet.Add(attack);
        skillSet.Add(attack);
        skillSet.Add(attack);
        EnemyHealing enemyHealing = new EnemyHealing();
        skillSet.Add(enemyHealing);
    }
}
