using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UkrainianStrength : PlayableSkill
{
    public UkrainianStrength()
    {
        Name = "Ukraiñska moc";
        SkillDescription = "budzi w sobie moc Swiet³any i przekazuje j¹ sojusznikowi, zwiêkszaj¹c mu atak i celnoœæ.";
        InFightDescription = " przekazuje moc Swiet³any ";
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
            return source.DativeName + " nie udaje siê obudziæ swojej ukraiñskiej mocy";
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
