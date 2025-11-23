using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleFingerKidBuffed : EnemyCharacter
{
    public MiddleFingerKidBuffed()
    {

        NominativeName = "Ch³opak z fakerem";
        DativeName = "Ch³opakowi z fakerem";
        AccusativeName = "Ch³opaka z fakerem";

        Health = MaxHealth = DefaultMaxHealth = BaseMaxHealth = 1000;
        DifficultyHealthChange = 300;
        Attack = DefaultAttack = BaseAttack = 360;
        DifficultyAttackChange = 140;
        Defense = DefaultDefense = BaseDefense = 60;
        Accuracy = DefaultAccuracy = BaseAccuracy = 0.85f;
        Turns = DefaultTurns = BaseTurns = 1;
        Speed = 450;
        MoneyDropped = 50;
        XPDropped = 2000;
        DoublePunch doublePunch = new DoublePunch();
        skillSet.Add(doublePunch);
    }
}
