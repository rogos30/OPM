using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fijolek : FriendlyCharacter
{
    //specialty - is immune to negative status effects
    public Fijolek() : base()
    {
        NominativeName = "Fijo³ek";
        DativeName = "Fijo³kowi";
        AccusativeName = "Fijo³ka";
        Health = MaxHealth = DefaultMaxHealth = 400;
        Skill = MaxSkill = 80;
        Attack = DefaultAttack = 80;
        Defense = DefaultDefense = 30;
        Accuracy = DefaultAccuracy = 1.1f;
        Turns = DefaultTurns = 1;
        Speed = 500;
        Attack attack = new Attack();
        skillSet.Add(attack);
    }
}
