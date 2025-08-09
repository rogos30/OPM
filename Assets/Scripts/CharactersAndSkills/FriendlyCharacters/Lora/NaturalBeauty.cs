using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaturalBeauty : PlayableSkill
{
    public NaturalBeauty()
    {
        Name = "Naturalna uroda";
        SkillDescription = "onie�miela przeciwnika, zmniejszaj�c jego obron�";
        InFightDescription = " zmniejsza obron� ";
        Cost = 70;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = false;
        AnimationId = 4;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return target.NominativeName + " opiera si� " + source.DativeName;
        }
        string finalDesc = source.NominativeName + InFightDescription + target.AccusativeName;
        int turns = 3;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 4;
        }
        finalDesc = finalDesc + " na " + (turns - 1) + " tury!";
        target.ApplyDebuff((int)Character.StatusEffects.DEFENSE, turns);
        return finalDesc;
    }
}
