using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : EnemyCharacter
{
    public Builder() : base()
    {
        NominativeName = "Budowniczy A";
        DativeName = "Budowniczemu A";
        AccusativeName = "Budowniczego A";
        Health = MaxHealth = DefaultMaxHealth = 4000;
        DifficultyHealthChange = 2000;
        Attack = DefaultAttack = 300;
        DifficultyAttackChange = 150;
        Defense = DefaultDefense = 70;
        Turns = DefaultTurns = 1;
        Speed = 350;
        MoneyDropped = 0;
        XPDropped = 0;
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

    public Builder(string nameN, string nameD, string nameA) : base()
    {
        NominativeName = nameN;
        DativeName = nameD;
        AccusativeName = nameA;
        Health = MaxHealth = DefaultMaxHealth = 4000;
        DifficultyHealthChange = 2000;
        Attack = DefaultAttack = 300;
        DifficultyAttackChange = 150;
        Defense = DefaultDefense = 70;
        Turns = DefaultTurns = 1;
        Speed = 350;
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
