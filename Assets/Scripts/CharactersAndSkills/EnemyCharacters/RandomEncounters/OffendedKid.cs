using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class OffendedKid : EnemyCharacter
{
    public OffendedKid()
    {
        NominativeName = "Dzieciak z fochem";
        DativeName = "Dzieciakowi z fochem";
        AccusativeName = "Dzieciaka z fochem";
        Health = MaxHealth = DefaultMaxHealth = BaseMaxHealth = 225;
        DifficultyHealthChange = 65;
        Attack = DefaultAttack = BaseAttack = 100;
        DifficultyAttackChange = 40;
        Defense = DefaultDefense = BaseDefense = 20;
        Accuracy = DefaultAccuracy = BaseAccuracy = 0.85f;
        Turns = DefaultTurns = BaseTurns = 1;
        Speed = 250;
        MoneyDropped = 15;
        XPDropped = 250;
        EnemyAttack attack = new EnemyAttack();
        skillSet.Add(attack);
    }
}
