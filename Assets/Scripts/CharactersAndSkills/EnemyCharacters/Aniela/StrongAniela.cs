using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongAniela : EnemyCharacter
{
    public StrongAniela() : base()
    {
        NominativeName = "Aniela";
        DativeName = "Anieli";
        AccusativeName = "Anielê";
        Health = MaxHealth = DefaultMaxHealth = BaseMaxHealth = 6000;
        DifficultyHealthChange = 3000;
        Attack = DefaultAttack = BaseAttack = 275;
        DifficultyAttackChange = 125;
        Defense = DefaultDefense = BaseDefense = 90;
        Accuracy = DefaultAccuracy = BaseAccuracy = 1f;
        Turns = DefaultTurns = BaseTurns = 1;
        Speed = 450;
        MoneyDropped = 200;
        XPDropped = 7500;
        DoublePunch doublePunch = new DoublePunch();
        skillSet.Add(doublePunch);
        skillSet.Add(doublePunch);
        StrongGrunt grunt = new StrongGrunt();
        skillSet.Add(grunt);
        Toxicisity toxicisity = new Toxicisity();
        skillSet.Add(toxicisity);
    }
}
