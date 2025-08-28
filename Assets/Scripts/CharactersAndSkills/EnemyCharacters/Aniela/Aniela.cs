using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aniela : EnemyCharacter
{
    public Aniela() : base()
    {
        NominativeName = "Aniela";
        DativeName = "Anieli";
        AccusativeName = "Anielê";
        Health = MaxHealth = DefaultMaxHealth = 1000;
        DifficultyHealthChange = 300;
        Attack = DefaultAttack = 90;
        DifficultyAttackChange = 40;
        Defense = DefaultDefense = 45;
        Accuracy = DefaultAccuracy = 0.9f;
        Turns = DefaultTurns = 1;
        Speed = 450;
        MoneyDropped = 10000;
        XPDropped = 10000;
        EnemyAttack attack = new EnemyAttack();
        skillSet.Add(attack);
        skillSet.Add(attack);
        Grunt grunt = new Grunt();
        skillSet.Add(grunt);
    }
}
