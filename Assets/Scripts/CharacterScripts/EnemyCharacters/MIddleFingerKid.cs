using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleFingerKid : EnemyCharacter
{
    // Start is called before the first frame update
    public MiddleFingerKid()
    {
        //EnemyCharacter character = new EnemyCharacter("Ch�opak z fakerem", 225, 30, 50, 15, 15, 0.85f, 1, 400, 20, 200, skills);

        NominativeName = "Ch�opak z fakerem";
        DativeName = "Ch�opakowi z fakerem";
        AccusativeName = "Ch�opaka z fakerem";
        Health = MaxHealth = DefaultMaxHealth = 225;
        DifficultyHealthChange = 30;
        Attack = DefaultAttack = 50;
        DifficultyAttackChange = 15;
        Defense = DefaultDefense = 15;
        Accuracy = DefaultAccuracy = 0.85f;
        Turns = DefaultTurns = 1;
        Speed = 400;
        MoneyDropped = 20;
        XPDropped = 200;
        EnemyAttack attack = new EnemyAttack();
        skillSet.Add(attack);
    }
}
