using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maja : FriendlyCharacter
{
    //specialty - items used by her have a 50% stronger effect
    public Maja() : base()
    {
        NominativeName = "Maja";
        DativeName = "Mai";
        AccusativeName = "Maj�";
        Health = MaxHealth = DefaultMaxHealth = 350;
        Skill = MaxSkill = 80;
        Attack = DefaultAttack = 70;
        Defense = DefaultDefense = 30;
        Accuracy = DefaultAccuracy = 0.9f;
        Turns = DefaultTurns = 1;
        Speed = 300;
        SpriteIndex = 3;
        ItemEnhancementMultiplier = 1.5f;
        AbilityDescription = "Przedmioty u�yte przez Maj� maj� o 50% zwi�kszony efekt";
        CharacterDescription = "Opis Mai wip";
        Attack attack = new Attack();
        skillSet.Add(attack);
        FrogThrow frogThrow = new FrogThrow();
        skillSet.Add(frogThrow);
        FrogHat frogHat = new FrogHat();
        skillSet.Add(frogHat);
        StrokeOfLuck strokeOfLuck = new StrokeOfLuck();
        skillSet.Add(strokeOfLuck);
        UkrainianStrength ukrainianStrength = new UkrainianStrength();
        skillSet.Add(ukrainianStrength);
        Kacper kacper = new Kacper();
        skillSet.Add(kacper);
    }
}
