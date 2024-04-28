using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoraleDebuff : PlayableSkill
{
    //skill = new Skill("Zepsucie morale", "demotywuje rywali, zmniejszaj¹c im celnoœæ", "zmniejsza celnoœæ", 110, 1, 0, 0.75f, 0, false, false, false, true, statusEffects);

    public MoraleDebuff() : base()
    {
        Name = "Gówniany monolog";
        SkillDescription = "jako Cesarz Kibla bierze siê za demotywacjê przeciwników";
        InFightDescription = " zmniejsza celnoœæ ";
        Cost = 110;
        AccuracyMultiplier = 0.75f;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = true;
        TargetIsRandom = false;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.DativeName + "nie udaje siê zdemotywowaæ przeciwników.";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName;
        int turns = 3;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 5;
        }
        finalDesc = finalDesc + " na " + turns + " tur!";
        target.ApplyDebuff(2, turns);
        return finalDesc;
    }
}
