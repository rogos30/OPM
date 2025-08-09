using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectGoalkeeper : PlayableSkill
{
    //* Perfekcyjny bramkarz � Je�eli ma na�o�ony debuff na obron�, to go zdejmuje. Nast�pnie zwi�ksza swoj� obron� na 3 tury.

    public PerfectGoalkeeper()
    {
        Name = "Super bramkarz";
        SkillDescription = "Je�eli ma na�o�ony debuff na obron�, to go zdejmuje. Nast�pnie zwi�ksza swoj� obron� na 3 tury";
        InFightDescription = " przygotowuje si� na strza�y przeciwnika na ";
        Cost = 0.2f;
        TargetIsFriendly = false;
        TargetIsSelf = true;
        MultipleTargets = false;
        TargetIsRandom = false;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.NominativeName + " wpuszcza banalnego gola.";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int turns = 3;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 5;
        }
        finalDesc = finalDesc + (turns-1) + " tury!";
        target.ApplyBuff(1, turns);
        target.ApplyBuff(1, turns);
        return finalDesc;
    }
}
