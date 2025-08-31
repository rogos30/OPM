using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UkrainianStrength : PlayableSkill
{
    public UkrainianStrength()
    {
        Name = "Ukrai�ska moc";
        SkillDescription = "budzi w sobie moc Swiet�any i przekazuje j� sojusznikowi, zwi�kszaj�c mu atak i celno��.";
        InFightDescription = " przekazuje moc Swiet�any ";
        Cost = 0.4f;
        TargetIsFriendly = true;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = false;
        SkillSoundId = 22;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.DativeName + " nie udaje si� obudzi� swojej ukrai�skiej mocy";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int turns = 3;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 5;
        }
        finalDesc = finalDesc + " " + (turns-1) + " tury!";
        target.ApplyBuff((int)Character.StatusEffects.ATTACK, turns);
        target.ApplyBuff((int)Character.StatusEffects.ACCURACY, turns);
        return finalDesc;
    }
}
