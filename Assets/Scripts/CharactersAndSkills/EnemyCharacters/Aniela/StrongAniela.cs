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
        Health = MaxHealth = DefaultMaxHealth = 6000;
        DifficultyHealthChange = 3000;
        Attack = DefaultAttack = 375;
        DifficultyAttackChange = 150;
        Defense = DefaultDefense = 90;
        Turns = DefaultTurns = 1;
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
