using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UkrainianStrength : PlayableSkill
{
    public UkrainianStrength()
    {
        Name = "Ukrai�ska moc";
        SkillDescription = "budzi w sobie moc Swiet�any i przekazuje j� sojusznikowi, zwi�kszaj�c mu atak i celno��.";
        InFightDescription = " przekazuje moc Swiet�any ";
        Cost = 0.4f;
        TargetIsFriendly = true;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = false;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.DativeName + " nie udaje si� obudzi� swojej ukrai�skiej mocy";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int turns = 3;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 5;
        }
        finalDesc = finalDesc + " " + turns + " tur!";
        target.ApplyBuff(0, turns);
        target.ApplyBuff(2, turns);
        return finalDesc;
    }
}
