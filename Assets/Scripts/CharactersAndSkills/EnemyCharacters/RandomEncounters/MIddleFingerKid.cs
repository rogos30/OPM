using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleFingerKid : EnemyCharacter
{
    public MiddleFingerKid()
    {
        //EnemyCharacter character = new EnemyCharacter("Ch�opak z fakerem", 225, 30, 50, 15, 15, 0.85f, 1, 400, 20, 200, skills);

        NominativeName = "Ch�opak z fakerem";
        DativeName = "Ch�opakowi z fakerem";
        AccusativeName = "Ch�opaka z fakerem";

        Health = MaxHealth = DefaultMaxHealth = BaseMaxHealth = 225;
        DifficultyHealthChange = 55;
        Attack = DefaultAttack = BaseAttack = 75;
        DifficultyAttackChange = 25;
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
