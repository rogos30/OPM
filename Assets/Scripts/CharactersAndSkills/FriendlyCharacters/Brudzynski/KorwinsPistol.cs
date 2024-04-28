using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KorwinsPistol : PlayableSkill
{
    //skill = new Skill("Sza³ szabli", "wykonuje 3 mocne ciêcia w losowych przeciwników", "wykonuje 3 mocne ciêcia", 0.5f, 3, 1.75f, 0.9f, 0, false, false, true, false, statusEffects);

    public KorwinsPistol()
    {
        Name = "Pistolet Korwina";
        SkillDescription = "pos³uguje siê broni¹ krula w celu rozprawienia siê z komunistami.";
        InFightDescription = " stawia kres lewactwu ";
        Cost = 240f;
        Repetitions = 3;
        AccuracyMultiplier = 1f;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = false;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            source.TakeDamage(source.Attack - source.Defense);
            return source.NominativeName + " trafia sam siebie!";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName;
        int damage = (int)(source.Attack * 1.75f * Random.Range(0.8f, 1.2f)) - target.Defense;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            damage *= source.criticalDamageMultiplier;
        }
        damage = Mathf.Max(damage, 1);
        finalDesc = finalDesc + " " + damage + " obra¿eñ!";
        target.TakeDamage(damage);
        return finalDesc;
    }
}
