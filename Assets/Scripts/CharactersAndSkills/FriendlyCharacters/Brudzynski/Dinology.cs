using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinology : PlayableSkill
{
    //skill = new Skill("Dinologia", "zanudza przeciwnika ciekawostkami o dinozaurach. Ciê¿ko trafiæ, ale parali¿uje.", "opowiada o dinozaurach", 0.35f, 1, 0, 0.5f, 0, false, false, false, false, statusEffects);

    public Dinology()
    {
        Name = "Dinologia";
        SkillDescription = "zanudza przeciwnika ciekawostkami o dinozaurach. Ciê¿ko trafiæ, ale parali¿uje.";
        InFightDescription = " opowiada o dinozaurach ";
        Cost = 0.35f;
        AccuracyMultiplier = 0.5f;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = false;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.DativeName + " nie zaciekawia " + target.AccusativeName + " (nic dziwnego)";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName;
        int turns = 3;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 4;
        }
        finalDesc = finalDesc + " i parali¿uje na " +  (turns-1) + " tury!";
        target.ApplyDebuff(4, turns);
        return finalDesc;
    }
}
