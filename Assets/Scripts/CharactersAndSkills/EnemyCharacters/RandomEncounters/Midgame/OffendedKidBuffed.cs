using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class OffendedKidBuffed : EnemyCharacter
{
    public OffendedKidBuffed()
    {
        NominativeName = "Dzieciak z fochem";
        DativeName = "Dzieciakowi z fochem";
        AccusativeName = "Dzieciaka z fochem";
        Health = MaxHealth = DefaultMaxHealth = BaseMaxHealth = 900;
        DifficultyHealthChange = 260;
        Attack = DefaultAttack = BaseAttack = 400;
        DifficultyAttackChange = 160;
        Defense = DefaultDefense = BaseDefense = 80;
        Accuracy = DefaultAccuracy = BaseAccuracy = 0.85f;
        Turns = DefaultTurns = BaseTurns = 1;
        Speed = 250;
        MoneyDropped = 40;
        XPDropped = 1000;
        EnemyAttack attack = new EnemyAttack();
        skillSet.Add(attack);
        HeavyPunch heavyPunch = new HeavyPunch();
        skillSet.Add(heavyPunch);
    }
}
