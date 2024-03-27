using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brudzynski : FriendlyCharacter
{
    //specialty - has 6 skills instead of 5 and unlocks them every 3 levels instead of 5

    public Brudzynski() : base()
    {
        NominativeName = "Brudzyñski";
        DativeName = "Brudzyñskiemu";
        AccusativeName = "Brudzyñskiego";
        Health = MaxHealth = DefaultMaxHealth = 450;
        Skill = MaxSkill = 100;
        Attack = DefaultAttack = 100;
        Defense = DefaultDefense = 30;
        Accuracy = DefaultAccuracy = 0.8f;
        Turns = DefaultTurns = 1;
        Speed = 200;
        levelsToUnlockSkill = 3;
        Attack attack = new Attack();
        skillSet.Add(attack);
    }
}
