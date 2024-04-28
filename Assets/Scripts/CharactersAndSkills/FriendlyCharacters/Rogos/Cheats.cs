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
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.NominativeName + " wpisuje niedzia³aj¹ce kody";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int turns = 3;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 5;
        }
        finalDesc = finalDesc + " " + turns + " tur!";
        target.ApplyBuff((int)Character.StatusEffects.TURNS, turns);
        return finalDesc;
    }
}
