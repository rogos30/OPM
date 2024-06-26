using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LushBangs : PlayableSkill
{

    public LushBangs()
    {
        Name = "Bujna grzywa";
        SkillDescription = "zarzuca grzyw� i zwi�ksza poczucie w�asnej warto�ci (i tym samym atak).";
        InFightDescription = " zarzuca grzyw� i zwi�ksza sobie atak na ";
        Cost = 135;
        TargetIsFriendly = false;
        TargetIsSelf = true;
        MultipleTargets = false;
        TargetIsRandom = false;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.DativeName + " spada peruka z g�owy";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int turns = 3;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 6;
        }
        finalDesc = finalDesc + " " + turns + " tur!";
        target.ApplyBuff((int)Character.StatusEffects.ATTACK, turns);
        return finalDesc;
    }
}
