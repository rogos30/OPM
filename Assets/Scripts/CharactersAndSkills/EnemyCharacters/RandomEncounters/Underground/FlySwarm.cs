using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class FlySwarm : EnemyCharacter
{
    public FlySwarm()
    {
        NominativeName = "Chmara much";
        DativeName = "Chmarze much";
        AccusativeName = "Chmarê much";
        Health = MaxHealth = DefaultMaxHealth = BaseMaxHealth = 1400;
        DifficultyHealthChange = 240;
        Attack = DefaultAttack = BaseAttack = 460;
        DifficultyAttackChange = 180;
        Defense = DefaultDefense = BaseDefense = 90;
        Accuracy = DefaultAccuracy = BaseAccuracy = 0.9f;
        Turns = DefaultTurns = BaseTurns = 1;
        Speed = 350;
        MoneyDropped = 50;
        XPDropped = 900;
        EnemyBite attack = new EnemyBite();
        skillSet.Add(attack);
        skillSet.Add(attack);
        HeavyBite heavyPunch = new HeavyBite();
        skillSet.Add(heavyPunch);
    }
}
