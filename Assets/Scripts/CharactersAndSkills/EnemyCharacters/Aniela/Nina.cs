using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nina : EnemyCharacter
{
    public Nina() : base()
    {
        NominativeName = "Nina";
        DativeName = "Ninie";
        AccusativeName = "Ninê";
        Health = MaxHealth = DefaultMaxHealth = 15000;
        DifficultyHealthChange = 1200;
        Attack = DefaultAttack = 400;
        DifficultyAttackChange = 100;
        Defense = DefaultDefense = 90;
        Accuracy = DefaultAccuracy = 0.9f;
        Turns = DefaultTurns = 1;
        Speed = 250;
        MoneyDropped = 10000;
        XPDropped = 10000;
        DoublePunch doublePunch = new DoublePunch();
        skillSet.Add(doublePunch);
        skillSet.Add(doublePunch);
        TriplePunch triplePunch = new TriplePunch();
        skillSet.Add(triplePunch);
        Idiocy idiocy = new Idiocy();
        skillSet.Add(idiocy);
    }
}
