using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator3 : EnemyCharacter
{
    public Generator3() : base()
    {
        NominativeName = "Generator teleskopowy";
        DativeName = "Generatorowi teleskopowemu";
        AccusativeName = "Generator teleskopowy";
        Health = MaxHealth = DefaultMaxHealth = 6000;
        DifficultyHealthChange = 3000;
        Attack = DefaultAttack = 250;
        DifficultyAttackChange = 80;
        Defense = DefaultDefense = 80;
        Accuracy = DefaultAccuracy = 0.90f;
        Turns = DefaultTurns = 1;
        Speed = 450;
        MoneyDropped = 100;
        XPDropped = 5000;
        DoublePunch doublePunch = new DoublePunch();
        skillSet.Add(doublePunch);
        skillSet.Add(doublePunch);
        PowerGeneration powerGeneration = new PowerGeneration();
        skillSet.Add(powerGeneration);
        GroupShock groupShock = new GroupShock();
        skillSet.Add(groupShock);
    }
}
