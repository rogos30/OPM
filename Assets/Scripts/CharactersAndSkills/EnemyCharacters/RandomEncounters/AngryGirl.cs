using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryGirl : EnemyCharacter
{
    //character = new EnemyCharacter("Wkurzona laska", 175, 25, 45, 10, 15, 0.95f, 1, 275, 20, 200, skills);

    public AngryGirl()
    {
        NominativeName = "Wkurzona laska";
        DativeName = "Wkurzonej lasce";
        AccusativeName = "Wkurzonej laski";
        Health = MaxHealth = DefaultMaxHealth = 175;
        DifficultyHealthChange = 40;
        Attack = DefaultAttack = 65;
        DifficultyAttackChange = 20;
        Defense = DefaultDefense = 15;
        Accuracy = DefaultAccuracy = 0.95f;
        Turns = DefaultTurns = 1;
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
