using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : EnemyCharacter
{
    public Spider()
    {

        NominativeName = "Ch³opak z fakerem";
        DativeName = "Ch³opakowi z fakerem";
        AccusativeName = "Ch³opaka z fakerem";

        Health = MaxHealth = DefaultMaxHealth = BaseMaxHealth = 2000;
        DifficultyHealthChange = 600;
        Attack = DefaultAttack = BaseAttack = 720;
        DifficultyAttackChange = 280;
        Defense = DefaultDefense = BaseDefense = 90;
        Accuracy = DefaultAccuracy = BaseAccuracy = 0.85f;
        Turns = DefaultTurns = BaseTurns = 1;
        Speed = 450;
        MoneyDropped = 50;
        XPDropped = 1000;
        EnemyBite attack = new EnemyBite();
        skillSet.Add(attack);
        HeavyBite heavyPunch = new HeavyBite();
        skillSet.Add(heavyPunch);
    }
}
