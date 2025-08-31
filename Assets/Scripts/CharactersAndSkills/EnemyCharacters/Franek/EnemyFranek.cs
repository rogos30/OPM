using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFranek : EnemyCharacter
{
    public EnemyFranek() : base()
    {
        NominativeName = "Franek";
        DativeName = "Frankowi";
        AccusativeName = "Franka";
        Health = MaxHealth = DefaultMaxHealth = 50000;
        DifficultyHealthChange = 15000;
        Attack = DefaultAttack = 350;
        DifficultyAttackChange = 150;
        Defense = DefaultDefense = 100;
        Turns = DefaultTurns = 2;
        Speed = 350;
        MoneyDropped = 5000;
        XPDropped = 500;

        EnemyAttack enemyAttack = new EnemyAttack();
        skillSet.Add(enemyAttack);
        skillSet.Add(enemyAttack);

        EnemyPerfectGoalkeeper enemyPerfectGoalkeeper = new EnemyPerfectGoalkeeper();
        skillSet.Add(enemyPerfectGoalkeeper);

        EnemyCrosswords enemyCrosswords = new EnemyCrosswords();
        skillSet.Add(enemyCrosswords);

        EnemyCounterattack enemyCounterattack = new EnemyCounterattack();
        skillSet.Add(enemyCounterattack);
        skillSet.Add(enemyCounterattack);

        DribbleThrow dribbleThrow = new DribbleThrow();
        skillSet.Add(dribbleThrow);
        skillSet.Add(dribbleThrow);
    }

    protected override void AdditionalChangesOnReset()
    {
        ApplyBuff((int)Character.StatusEffects.ATTACK, 4);
        ApplyBuff((int)Character.StatusEffects.DEFENSE, 4);
        ApplyBuff((int)Character.StatusEffects.ACCURACY, 4);
    }
}
