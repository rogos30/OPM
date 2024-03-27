using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kaja : FriendlyCharacter
{
    //specialty - items used by her have a 50% stronger effect
    public Kaja() : base()
    {
        NominativeName = "Kaja";
        DativeName = "Kai";
        AccusativeName = "Kajê";
        Health = MaxHealth = DefaultMaxHealth = 350;
        Skill = MaxSkill = 80;
        Attack = DefaultAttack = 70;
        Defense = DefaultDefense = 30;
        Accuracy = DefaultAccuracy = 0.9f;
        Turns = DefaultTurns = 1;
        Speed = 300;
        Attack attack = new Attack();
        skillSet.Add(attack);
    }
}
