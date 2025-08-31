using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogHat : PlayableSkill
{
    public FrogHat() : base()
    {
        Name = "¯abia czapka";
        SkillDescription = "zak³ada swoj¹ ¿abi¹ czapkê, nak³adaj¹c na siebie obronê i regeneracjê.";
        InFightDescription = " zak³ada swoj¹ ¿abê-czapkê ";
        Cost = 120;
        TargetIsFriendly = false;
        TargetIsSelf = true;
        MultipleTargets = false;
        TargetIsRandom = false;
        SkillSoundId = 25;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.NominativeName + " nie trafia ¿ab¹ na swoj¹ g³owê...";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int turns = 3;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 5;
        }
        finalDesc = finalDesc + " i nak³ada na siebie obronê i regeneracjê na " + (turns-1) + " tury!";
        target.ApplyBuff((int)Character.StatusEffects.DEFENSE, turns);
        target.ApplyBuff((int)Character.StatusEffects.HEALTH, turns);
        return finalDesc;
    }
}
