using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogHat : PlayableSkill
{
    public FrogHat() : base()
    {
        Name = "�abia czapka";
        SkillDescription = "zak�ada swoj� �abi� czapk�, nak�adaj�c na siebie obron� i regeneracj�.";
        InFightDescription = " zak�ada swoj� �ab�-czapk� ";
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
            return source.NominativeName + " nie trafia �ab� na swoj� g�ow�...";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int turns = 3;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 5;
        }
        finalDesc = finalDesc + " i nak�ada na siebie obron� i regeneracj� na " + (turns-1) + " tury!";
        target.ApplyBuff((int)Character.StatusEffects.DEFENSE, turns);
        target.ApplyBuff((int)Character.StatusEffects.HEALTH, turns);
        return finalDesc;
    }
}
