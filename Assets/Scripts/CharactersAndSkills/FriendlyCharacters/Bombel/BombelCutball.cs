using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombelCutball : PlayableSkill
{
    public BombelCutball()
    {
        Name = "Œcina";
        SkillDescription = "œcina pi³eczkê w przeciwnika. Wysokie obra¿enia, ale ciê¿ko trafiæ.";
        InFightDescription = " œcina pi³eczkê i zadaje przeciwnikowi ";
        Cost = 120;
        AccuracyMultiplier = 0.4f;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = false;
        SkillSoundId = 16;

    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.NominativeName + " nie trafia w pi³eczkê";
        }
        int upgrade = Random.Range(0, 5);
        if (upgrade == 0)
        {
            skillPerformance++;
            Mathf.Min(skillPerformance, 2);
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int damage = (int)(source.Attack * 3 * Random.Range(0.8f, 1.2f)) - target.Defense;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            damage *= source.criticalDamageMultiplier;
        }
        damage = Mathf.Max(damage, 1);
        finalDesc = finalDesc + damage + " obra¿eñ!";
        target.TakeDamage(damage);
        return finalDesc;
    }
}
