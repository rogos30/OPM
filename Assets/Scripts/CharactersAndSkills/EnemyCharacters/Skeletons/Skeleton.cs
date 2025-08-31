using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : EnemyCharacter
{
    public Skeleton() : base()
    {
        NominativeName = "Szkielet - ¿o³nierz";
        DativeName = "Szkieletowi - ¿o³nierzowi";
        AccusativeName = "Szkieleta - ¿o³nierza";
        Health = MaxHealth = DefaultMaxHealth = 20000;
        DifficultyHealthChange = 2500;
        Attack = DefaultAttack = 350;
        DifficultyAttackChange = 170;
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
