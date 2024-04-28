using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutball : PlayableSkill
{
    //skill = new Skill("Œcina", "œcina pi³eczkê w przeciwnika. Wysokie obra¿enia, ale ciê¿ko trafiæ.", "œcina pi³eczkê", 150, 1, 3, 0.4f, 0, false, false, false, false, statusEffects);

    public Cutball()
    {
        Name = "Œcina";
        SkillDescription = "œcina pi³eczkê w przeciwnika. Wysokie obra¿enia, ale ciê¿ko trafiæ.";
        InFightDescription = " œcina pi³eczkê i zadaje przeciwnikowi ";
        Cost = 150;
        AccuracyMultiplier = 0.4f;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = false;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.NominativeName + " nie trafia w pi³eczkê";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int damage = (int)(source.Attack * 3 * Random.Range(0.8f, 1.2f)) - target.Defense;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            damage *= source.criticalDamageMultiplier;
        }
        finalDesc = finalDesc + damage + " obra¿eñ!";
        target.TakeDamage(damage);
        return finalDesc;
    }
}
