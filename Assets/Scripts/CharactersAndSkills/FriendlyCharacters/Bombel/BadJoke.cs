using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadJoke : PlayableSkill
{
    public BadJoke()
    {
        Name = "Kiepski dowcip";
        SkillDescription = "opowiada kiepski dowcip i jako jedyny siê z niego œmieje, trac¹c jednak koncentracjê. Wzmacnia w³asny atak kosztem celnoœci";
        InFightDescription = " wzmacnia swój atak na ";
        Cost = 65;
        TargetIsFriendly = false;
        TargetIsSelf = true;
        MultipleTargets = false;
        TargetIsRandom = false;
        SkillSoundId = 19;
        IsUnlocked = true;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        int upgrade = Random.Range(0, 5);
        if (upgrade == 0)
        {
            skillPerformance++;
            Mathf.Min(skillPerformance, 2);
        }
        if (skillPerformance == 0)
        {
            return source.NominativeName + " zacz¹³ siê agresywnie j¹kaæ i nie dokoñczy³ ¿artu...";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int attackTurns = 3, accuracyTurns = 3;
        if (skillPerformance == 2)
        {
            attackTurns = 5;
            accuracyTurns = 2;
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
        }
        target.ApplyBuff((int)Character.StatusEffects.ATTACK, attackTurns);
        target.ApplyDebuff((int)Character.StatusEffects.ACCURACY, accuracyTurns);
        finalDesc = finalDesc + (attackTurns - 1) + ", jednoczeœnie os³abiaj¹c swoj¹ celnoœæ na " + (accuracyTurns-1) + " tury!";
        return finalDesc;
    }

    public override void upgrade()
    {
        return;
    }
}
