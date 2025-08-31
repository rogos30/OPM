using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : PlayableSkill
{

    public Cheats()
    {
        Name = "Cheaty";
        SkillDescription = "oszukuje i daje sobie dodatkowy ruch w turze na kilka tur.";
        InFightDescription = " wpisuje cheaty i daje sobie dodatkowy ruch na ";
        Cost = 0.3f;
        TargetIsFriendly = false;
        TargetIsSelf = true;
        MultipleTargets = false;
        TargetIsRandom = false;
        SkillSoundId = 3;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.NominativeName + " wpisuje niedzia³aj¹ce kody";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int turns = 4;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 6;
            finalDesc = finalDesc + (turns - 1) + " tur!";
        }
        else
        {
            finalDesc = finalDesc + (turns - 1) + " tury!";
        }
        target.ApplyBuff((int)Character.StatusEffects.TURNS, turns);
        return finalDesc;
    }
}
