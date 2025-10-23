using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : EnemyCharacter
{
    public Skeleton() : base()
    {
        NominativeName = "Szkielet - �o�nierz";
        DativeName = "Szkieletowi - �o�nierzowi";
        AccusativeName = "Szkieleta - �o�nierza";
        Health = MaxHealth = DefaultMaxHealth = 8000;
        DifficultyHealthChange = 4000;
        Attack = DefaultAttack = 550;
        DifficultyAttackChange = 250;
        Defense = DefaultDefense = 90;
        Turns = DefaultTurns = 1;
        Speed = 350;
        MoneyDropped = 500;
        XPDropped = 5000;
        HeavyPunch heavyPunch = new HeavyPunch();
        skillSet.Add(heavyPunch);
        ToxicGas toxicGas = new ToxicGas();
        skillSet.Add(toxicGas);
        Staredown staredown = new Staredown();
        skillSet.Add(staredown);
        BoneThrow boneThrow = new BoneThrow();
        skillSet.Add(boneThrow);
    }

    public Skeleton(string nameN, string nameD, string nameA) : base()
    {
        NominativeName = nameN;
        DativeName = nameD;
        AccusativeName = nameA;
        Health = MaxHealth = DefaultMaxHealth = 10000;
        DifficultyHealthChange = 5000;
        Attack = DefaultAttack = 550;
        DifficultyAttackChange = 250;
        Defense = DefaultDefense = 90;
        Turns = DefaultTurns = 1;
        Speed = 350;
        MoneyDropped = 5000;
        XPDropped = 500;
        HeavyPunch heavyPunch = new HeavyPunch();
        skillSet.Add(heavyPunch);
        ToxicGas toxicGas = new ToxicGas();
        skillSet.Add(toxicGas);
        Staredown staredown = new Staredown();
        skillSet.Add(staredown);
        BoneThrow boneThrow = new BoneThrow();
        skillSet.Add(boneThrow);
    }
}
