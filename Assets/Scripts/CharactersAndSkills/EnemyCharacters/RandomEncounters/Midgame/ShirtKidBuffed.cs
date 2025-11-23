using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ShirtKidBuffed : EnemyCharacter
{
    public ShirtKidBuffed()
    {
        NominativeName = "Dzieciak w koszuli";
        DativeName = "Dzieciakowi w koszuli";
        AccusativeName = "Dzieciaka w koszuli";
        Health = MaxHealth = DefaultMaxHealth = BaseMaxHealth = 700;
        DifficultyHealthChange = 240;
        Attack = DefaultAttack = BaseAttack = 460;
        DifficultyAttackChange = 180;
        Defense = DefaultDefense = BaseDefense = 60;
        Accuracy = DefaultAccuracy = BaseAccuracy = 0.9f;
        Turns = DefaultTurns = BaseTurns = 1;
        Speed = 350;
        MoneyDropped = 70;
        XPDropped = 600;
        EnemyAttack attack = new EnemyAttack();
        skillSet.Add(attack);
        HeavyPunch heavyPunch = new HeavyPunch();
        skillSet.Add(heavyPunch);
    }
}
