using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleFingerKid : EnemyCharacter
{
    // Start is called before the first frame update
    public MiddleFingerKid()
    {
        //EnemyCharacter character = new EnemyCharacter("Ch這pak z fakerem", 225, 30, 50, 15, 15, 0.85f, 1, 400, 20, 200, skills);

        NominativeName = "Ch這pak z fakerem";
        DativeName = "Ch這pakowi z fakerem";
        AccusativeName = "Ch這paka z fakerem";
        Health = MaxHealth = DefaultMaxHealth = 225;
        DifficultyHealthChange = 55;
        Attack = DefaultAttack = 75;
        DifficultyAttackChange = 25;
        Defense = DefaultDefense = 15;
        Accuracy = DefaultAccuracy = 0.85f;
        Turns = DefaultTurns = 1;
        Speed = 450;
        MoneyDropped = 20;
        XPDropped = 500;
        EnemyAttack attack = new EnemyAttack();
        skillSet.Add(attack);
    }
}
