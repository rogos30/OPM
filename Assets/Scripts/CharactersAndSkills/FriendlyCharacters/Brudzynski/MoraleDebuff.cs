using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoraleDebuff : PlayableSkill
{
    //skill = new Skill("Zepsucie morale", "demotywuje rywali, zmniejszaj�c im celno��", "zmniejsza celno��", 110, 1, 0, 0.75f, 0, false, false, false, true, statusEffects);

    public MoraleDebuff() : base()
    {
        Name = "G�wniany monolog";
        SkillDescription = "jako Cesarz Kibla bierze si� za demotywacj� przeciwnik�w";
        InFightDescription = " zmniejsza celno�� ";
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
            return source.DativeName + "nie udaje si� zdemotywowa� przeciwnik�w.";
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
