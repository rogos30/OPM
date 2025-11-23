using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class IndianKid : EnemyCharacter
{
    public IndianKid()
    {
        NominativeName = "Dzieciak w koszuli";
        DativeName = "Dzieciakowi w koszuli";
        AccusativeName = "Dzieciaka w koszuli";
        Health = MaxHealth = DefaultMaxHealth = BaseMaxHealth = 175;
        DifficultyHealthChange = 60;
        Attack = DefaultAttack = BaseAttack = 115;
        DifficultyAttackChange = 45;
        Defense = DefaultDefense = BaseDefense = 15;
        Accuracy = DefaultAccuracy = BaseAccuracy = 0.9f;
        Turns = DefaultTurns = BaseTurns = 1;
        Speed = 350;
        MoneyDropped = 25;
        XPDropped = 150;
        EnemyAttack attack = new EnemyAttack();
        skillSet.Add(attack);
        skillSet.Add(attack);
        skillSet.Add(attack);
        HeavyPunch heavyPunch = new HeavyPunch();
        skillSet.Add(heavyPunch);
    }
}
