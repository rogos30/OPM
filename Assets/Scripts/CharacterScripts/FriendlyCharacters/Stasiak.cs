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
        Health = MaxHealth = DefaultMaxHealth = 450;
        Skill = MaxSkill = 100;
        Attack = DefaultAttack = 110;
        Defense = DefaultDefense = 30;
        Accuracy = DefaultAccuracy = 1;
        Turns = DefaultTurns = 1;
        Speed = 400;
        criticalDamageMultiplier = 3;
        Attack attack = new Attack();
        skillSet.Add(attack);
    }
}
