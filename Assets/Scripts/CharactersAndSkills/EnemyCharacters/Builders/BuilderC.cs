using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderC : EnemyCharacter
{
    public BuilderC() : base()
    {
        NominativeName = "Budowniczy C";
        DativeName = "Budowniczemu C";
        AccusativeName = "Budowniczego C";
        Health = MaxHealth = DefaultMaxHealth = 10000;
        DifficultyHealthChange = 1000;
        Attack = DefaultAttack = 150;
        DifficultyAttackChange = 80;
        Defense = DefaultDefense = 70;
        Turns = DefaultTurns = 1;
        Speed = 330;
        MoneyDropped = 5000;
        XPDropped = 500;
        ShovelStrike shovelStrike = new ShovelStrike();
        skillSet.Add(shovelStrike);
        skillSet.Add(shovelStrike);
        DrinkOfTheGods drinkOfTheGods = new DrinkOfTheGods();
        skillSet.Add(drinkOfTheGods);
        AlcoholOverdose alcoholOverdose = new AlcoholOverdose();
        skillSet.Add(alcoholOverdose);
        Wheelbarrow wheelbarrow = new Wheelbarrow();
        skillSet.Add(wheelbarrow);
        skillSet.Add(wheelbarrow);
        BottleThrow bottleThrow = new BottleThrow();
        skillSet.Add(bottleThrow);
    }
}
