using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracksuiter : EnemyCharacter
{
    public Tracksuiter() : base()
    {
        NominativeName = "Dresiarz";
        DativeName = "Dresiarzowi";
        AccusativeName = "Dresiarza";
        Health = MaxHealth = DefaultMaxHealth = BaseMaxHealth = 5000;
        DifficultyHealthChange = 1750;
        Attack = DefaultAttack = BaseAttack = 200;
        DifficultyAttackChange = 80;
        Defense = DefaultDefense = BaseDefense = 70;
        Accuracy = DefaultAccuracy = BaseAccuracy = 1f;
        Turns = DefaultTurns = BaseTurns = 1;
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

    public Tracksuiter(string nameN, string nameD, string nameA) : base()
    {
        NominativeName = nameN;
        DativeName = nameD;
        AccusativeName = nameA;
        Health = MaxHealth = DefaultMaxHealth = BaseMaxHealth = 6000;
        DifficultyHealthChange = 2000;
        Attack = DefaultAttack = BaseAttack = 250;
        DifficultyAttackChange = 80;
        Defense = DefaultDefense = BaseDefense = 70;
        Accuracy = DefaultAccuracy = BaseAccuracy = 1f;
        Turns = DefaultTurns = 1;
        Speed = 350;
        MoneyDropped = 5000;
        XPDropped = 500;
        BaseballStrike baseballStrike = new BaseballStrike();
        skillSet.Add(baseballStrike);
        skillSet.Add(baseballStrike);
        BottleThrow bottleThrow = new BottleThrow();
        skillSet.Add(bottleThrow);
        EnemyBalaclava enemyBalaclava = new EnemyBalaclava();
        skillSet.Add(enemyBalaclava);
        Chant chant = new Chant();
        skillSet.Add(chant);
    }
}
