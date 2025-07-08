using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongKinga : EnemyCharacter
{
    public StrongKinga() : base()
    {
        NominativeName = "Kinga";
        DativeName = "Kindze";
        AccusativeName = "Kingê";
        Health = MaxHealth = DefaultMaxHealth = 18000;
        DifficultyHealthChange = 1500;
        Attack = DefaultAttack = 350;
        DifficultyAttackChange = 80;
        Defense = DefaultDefense = 90;
        Turns = DefaultTurns = 1;
        Speed = 450;
        MoneyDropped = 10000;
        XPDropped = 10000;
        DoublePunch doublePunch = new DoublePunch();
        skillSet.Add(doublePunch);
        skillSet.Add(doublePunch);
        StrongGrunt grunt = new StrongGrunt();
        skillSet.Add(grunt);
        Toxicisity toxicisity = new Toxicisity();
        skillSet.Add(toxicisity);
    }
}
