using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleFingerKid : EnemyCharacter
{
    public MiddleFingerKid()
    {
        NominativeName = "M³odszy uczeñ";
        DativeName = "M³odszemu uczniowi";
        AccusativeName = "M³odszego ucznia";

        Health = MaxHealth = DefaultMaxHealth = BaseMaxHealth = 250;
        DifficultyHealthChange = 75;
        Attack = DefaultAttack = BaseAttack = 90;
        DifficultyAttackChange = 35;
        Defense = DefaultDefense = BaseDefense = 15;
        Accuracy = DefaultAccuracy = BaseAccuracy = 0.85f;
        Turns = DefaultTurns = BaseTurns = 1;
        Speed = 450;
        MoneyDropped = 20;
        XPDropped = 500;
        EnemyAttack attack = new EnemyAttack();
        skillSet.Add(attack);
    }
}
