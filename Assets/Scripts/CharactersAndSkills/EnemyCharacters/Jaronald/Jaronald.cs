using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jaronald : EnemyCharacter
{
    public Jaronald() : base()
    {
        NominativeName = "Jaronald";
        DativeName = "Jaronaldowi";
        AccusativeName = "Jaronalda";
        Health = MaxHealth = DefaultMaxHealth = BaseMaxHealth = 5000;
        DifficultyHealthChange = 5000;
        Attack = DefaultAttack = BaseAttack = 300;
        DifficultyAttackChange = 100;
        Defense = DefaultDefense = BaseDefense = 70;
        Accuracy = DefaultAccuracy = BaseAccuracy = 0.90f;
        Turns = DefaultTurns = BaseTurns = 1;
        Speed = 300;
        MoneyDropped = 0;
        XPDropped = 0;
        EnemyAttack enemyAttack = new EnemyAttack();
        skillSet.Add(enemyAttack);
        EnemyCutball enemyCutball = new EnemyCutball();
        skillSet.Add(enemyCutball);
        EnemyCheats enemyCheats = new EnemyCheats();
        skillSet.Add(enemyCheats);
        Toxicisity toxicisity = new Toxicisity();
        skillSet.Add(toxicisity);
    }
}
