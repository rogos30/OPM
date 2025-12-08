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
        Health = MaxHealth = DefaultMaxHealth = BaseMaxHealth = 3000;
        DifficultyHealthChange = 1700;
        Attack = DefaultAttack = BaseAttack = 250;
        DifficultyAttackChange = 125;
        Defense = DefaultDefense = BaseDefense = 70;
        Accuracy = DefaultAccuracy = BaseAccuracy = 1f;
        Turns = DefaultTurns = BaseTurns = 1;
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
        Health = MaxHealth = DefaultMaxHealth = BaseMaxHealth = 3000;
        DifficultyHealthChange = 1700;
        Attack = DefaultAttack = BaseAttack = 250;
        DifficultyAttackChange = 125;
        Defense = DefaultDefense = BaseDefense = 70;
        Accuracy = DefaultAccuracy = BaseAccuracy = 1f;
        Turns = DefaultTurns = BaseTurns = 1;
        Speed = 350;
        MoneyDropped = 50;
        XPDropped = 500;
        ShovelStrike shovelStrike = new ShovelStrike();
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
        skillSet.Add(bottleThrow);
    }
}
