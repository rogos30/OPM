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
        Health = MaxHealth = DefaultMaxHealth = 10000;
        DifficultyHealthChange = 1000;
        Attack = DefaultAttack = 150;
        DifficultyAttackChange = 80;
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

    public Tracksuiter(string nameN, string nameD, string nameA) : base()
    {
        NominativeName = nameN;
        DativeName = nameD;
        AccusativeName = nameA;
        Health = MaxHealth = DefaultMaxHealth = 7000;
        DifficultyHealthChange = 1000;
        Attack = DefaultAttack = 150;
        DifficultyAttackChange = 80;
        Defense = DefaultDefense = 70;
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
