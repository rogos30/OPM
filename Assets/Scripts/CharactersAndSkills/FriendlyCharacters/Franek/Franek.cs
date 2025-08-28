using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.AudioSettings;

public class Franek : FriendlyCharacter
{
    //specialty - tbd

    public Franek() : base()
    {
        NominativeName = "Franek";
        DativeName = "Frankowi";
        AccusativeName = "Franka";
        Health = MaxHealth = DefaultMaxHealth = 400;
        Skill = MaxSkill = 80;
        Attack = DefaultAttack = 75;
        Defense = DefaultDefense = 50;
        Accuracy = DefaultAccuracy = 1.2f;
        Turns = DefaultTurns = 1;
        Speed = 500;
        SpriteIndex = 11;
        AbilityDescription = "Franek zaczyna walk� z buffem do ataku, obrony i celno�ci na 3 tury";
        CharacterDescription = "Opis Franka wip";
        Attack attack = new Attack();
        skillSet.Add(attack);
        PerfectGoalkeeper perfectGoalkeeper = new PerfectGoalkeeper();
        skillSet.Add(perfectGoalkeeper);
        Crosswords crosswords = new Crosswords();
        skillSet.Add(crosswords);
        Counterattack counterattack = new Counterattack();
        skillSet.Add(counterattack);
    }

    protected override void AdditionalChangesOnReset()
    {
        base.AdditionalChangesOnReset();
        ApplyBuff((int)Character.StatusEffects.ATTACK, 4);
        ApplyBuff((int)Character.StatusEffects.DEFENSE, 4);
        ApplyBuff((int)Character.StatusEffects.ACCURACY, 4);
    }
}
