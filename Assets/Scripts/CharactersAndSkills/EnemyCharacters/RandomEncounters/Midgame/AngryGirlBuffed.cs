using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryGirlBuffed : EnemyCharacter
{
    public AngryGirlBuffed()
    {
        NominativeName = "Wkurzona laska";
        DativeName = "Wkurzonej lasce";
        AccusativeName = "Wkurzonej laski";
        Health = MaxHealth = DefaultMaxHealth = BaseMaxHealth = 800;
        DifficultyHealthChange = 200;
        Attack = DefaultAttack = BaseAttack = 300;
        DifficultyAttackChange = 120;
        Defense = DefaultDefense = BaseDefense = 60;
        Accuracy = DefaultAccuracy = BaseAccuracy = 0.95f;
        Turns = DefaultTurns = BaseTurns = 1;
        Speed = 275;
        MoneyDropped = 50;
        XPDropped = 800;
        EnemyAttack attack = new EnemyAttack();
        skillSet.Add(attack);
        skillSet.Add(attack);
        EnemyHealing enemyHealing = new EnemyHealing();
        skillSet.Add(enemyHealing);
    }
}
