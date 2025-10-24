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
        Health = MaxHealth = DefaultMaxHealth = BaseMaxHealth = 5000;
        DifficultyHealthChange = 2500;
        Attack = DefaultAttack = BaseAttack = 325;
        DifficultyAttackChange = 150;
        Defense = DefaultDefense = BaseDefense = 90;
        Accuracy = DefaultAccuracy = BaseAccuracy = 0.9f;
        Turns = DefaultTurns = BaseTurns = 1;
        Speed = 250;
        MoneyDropped = 0;
        XPDropped = 0;
        DoublePunch doublePunch = new DoublePunch();
        skillSet.Add(doublePunch);
        skillSet.Add(doublePunch);
        TriplePunch triplePunch = new TriplePunch();
        skillSet.Add(triplePunch);
        Idiocy idiocy = new Idiocy();
        skillSet.Add(idiocy);
    }
}
