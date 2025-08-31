using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peter : EnemyCharacter
{
    public Peter() : base()
    {
        NominativeName = "Piotrek";
        DativeName = "Piotrkowi";
        AccusativeName = "Piotrka";
        Health = MaxHealth = DefaultMaxHealth = 5000;
        DifficultyHealthChange = 1500;
        Attack = DefaultAttack = 150;
        DifficultyAttackChange = 70;
        Defense = DefaultDefense = 75;
        Accuracy = DefaultAccuracy = 0.9f;
        Turns = DefaultTurns = 1;
        Speed = 150;
        MoneyDropped = 1;
        XPDropped = 1;
        DoublePunch doublePunch = new DoublePunch();
        skillSet.Add(doublePunch);
        TriplePunch triplePunch = new TriplePunch();
        skillSet.Add(triplePunch);
        HeavyPunch heavyPunch = new HeavyPunch();
        skillSet.Add(heavyPunch);
    }
}
