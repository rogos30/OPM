using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBuiler : EnemyCharacter
{
    public MainBuiler() : base()
    {
        NominativeName = "Brygadzista";
        DativeName = "Brygadziœcie";
        AccusativeName = "Brygadzisty";
        Health = MaxHealth = DefaultMaxHealth = 15000;
        DifficultyHealthChange = 1500;
        Attack = DefaultAttack = 150;
        DifficultyAttackChange = 80;
        Defense = DefaultDefense = 100;
        Turns = DefaultTurns = 1;
        Speed = 450;
        MoneyDropped = 5000;
        XPDropped = 500;
        EnemyMoraleBoost enemyMoraleBoost = new EnemyMoraleBoost();
        skillSet.Add(enemyMoraleBoost);
        ShovelStrike shovelStrike = new ShovelStrike();
        skillSet.Add(shovelStrike);
        Wheelbarrow wheelbarrow = new Wheelbarrow();
        skillSet.Add(wheelbarrow);
        BottleThrow bottleThrow = new BottleThrow();
        skillSet.Add(bottleThrow);
    }
}
