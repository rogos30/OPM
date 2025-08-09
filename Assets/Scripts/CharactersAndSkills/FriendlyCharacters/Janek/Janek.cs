using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.AudioSettings;

public class Janek : FriendlyCharacter
{
    //specialty - tbd

    public Janek() : base()
    {
        NominativeName = "Janek";
        DativeName = "Jankowi";
        AccusativeName = "Janka";
        Health = MaxHealth = DefaultMaxHealth = 400;
        Skill = MaxSkill = 80;
        Attack = DefaultAttack = 75;
        Defense = DefaultDefense = 50;
        Accuracy = DefaultAccuracy = 1.2f;
        Turns = DefaultTurns = 1;
        Speed = 500;
        SpriteIndex = 11;
        AbilityDescription = "Janek zaczyna walkê z buffem do ataku, obrony i celnoœci na 3 tury";
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
