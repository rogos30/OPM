using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectGoalkeeper : PlayableSkill
{
    //* Perfekcyjny bramkarz – Je¿eli ma na³o¿ony debuff na obronê, to go zdejmuje. Nastêpnie zwiêksza swoj¹ obronê na 3 tury.

    public PerfectGoalkeeper()
    {
        Name = "Perfekcyjny bramkarz";
        SkillDescription = "Je¿eli ma na³o¿ony debuff na obronê, to go zdejmuje. Nastêpnie zwiêksza swoj¹ obronê na 3 tury";
        InFightDescription = " przygotowuje siê na strza³y przeciwnika na ";
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
            return source.NominativeName + " wpuszcza banalnego gola.";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int turns = 3;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 5;
        }
        finalDesc = finalDesc + turns + " tur!";
        target.ApplyBuff(1, turns);
        ((Swietlik)source).ReduceBetrayal();
        return finalDesc;
    }
}
