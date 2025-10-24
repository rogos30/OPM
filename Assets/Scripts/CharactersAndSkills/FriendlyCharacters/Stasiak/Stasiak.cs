using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stasiak : FriendlyCharacter
{
    //specialty - cricital hits deal 3x the damage instead of 2x
    public Stasiak() : base()
    {
        NominativeName = "Stasiak";
        DativeName = "Stasiakowi";
        AccusativeName = "Stasiaka";

        Health = MaxHealth = DefaultMaxHealth = BaseMaxHealth = 450;
        Skill = MaxSkill = 100;
        Attack = DefaultAttack = BaseAttack = 110;
        Defense = DefaultDefense = BaseDefense = 30;
        Accuracy = DefaultAccuracy = BaseAccuracy = 1;

        Turns = DefaultTurns = 1;
        Speed = 400;
        SpriteIndex = 2;
        criticalDamageMultiplier = 3;
        AbilityDescription = "Krytyczne trafienia Stasiaka daj¹ 3x efekt zamiast 2x";
        CharacterDescription = "Opis Stasiaka wip";
        Attack attack = new Attack();
        skillSet.Add(attack);
        Overrun overrun = new Overrun();
        skillSet.Add(overrun);
        LushBangs lushBangs = new LushBangs();
        skillSet.Add(lushBangs);
        Curveball curveball = new Curveball();
        skillSet.Add(curveball);
        Doubles doubles = new Doubles();
        skillSet.Add(doubles);
        Cutball cutball = new Cutball();
        skillSet.Add(cutball);
    }
}
