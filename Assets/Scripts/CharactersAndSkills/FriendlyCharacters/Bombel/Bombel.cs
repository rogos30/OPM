using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombel : FriendlyCharacter
{
    public Bombel() : base()
    {
        NominativeName = "Mati";
        DativeName = "Matiemu";
        AccusativeName = "Matiego";

        Health = MaxHealth = DefaultMaxHealth = BaseMaxHealth = 420;
        Skill = MaxSkill = 120;
        Attack = DefaultAttack = BaseAttack = 100;
        Defense = DefaultDefense = BaseDefense = 30;
        Accuracy = DefaultAccuracy = BaseAccuracy = 0.95f;

        CanBeUpgraded = false;
        Turns = DefaultTurns = 1;
        Speed = 400;
        SpriteIndex = 13;
        criticalDamageMultiplier = 7;
        AbilityDescription = "Chybienie umiejêtnoœci¹ Matiego ma 20% szans trafiæ, a zwyk³e trafienie ma 20% szans staæ siê krytycznym trafieniem.";
        CharacterDescription = "Opis Matiego wip";
        BombelAttack attack = new();
        skillSet.Add(attack);
        BombelCurveball curveball = new();
        skillSet.Add(curveball);
        BombelCutball cutball = new();
        skillSet.Add(cutball);
        BadJoke badJoke = new();
        skillSet.Add(badJoke);
    }
}
