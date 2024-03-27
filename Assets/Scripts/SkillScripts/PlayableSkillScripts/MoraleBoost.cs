using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoraleBoost : PlayableSkill
{ //skill = new Skill("Podbicie morale", "zwiêksza morale w dru¿ynie, zwiêkszaj¹c celnoœæ sojusznikom", "zwiêksza celnoœæ", 50, 1, 0, 1, 0, true, false, false, true, statusEffects);

    public MoraleBoost() : base()
    {
        Name = "Podbicie morale";
        SkillDescription = "zwiêksza morale w dru¿ynie, zwiêkszaj¹c celnoœæ sojusznikom";
        InFightDescription = "zwiêksza celnoœæ ";
        Cost = 50;
        TargetIsFriendly = true;
        TargetIsSelf = false;
        MultipleTargets = true;
        TargetIsRandom = false;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.DativeName + "nie udaje siê pocieszyæ dru¿yny";
        }
        string finalDesc = source.NominativeName + InFightDescription + source.DativeName;
        int turns = 3;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 5;
        }
        finalDesc = finalDesc + " na " + turns + " tur!";
        target.StatusTimers[2] = turns;
        return finalDesc;
    }
}
