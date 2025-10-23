using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMayhem : PlayableSkill
{
    //skill = new Skill("Sza³ szabli", "wykonuje 3 mocne ciêcia w losowych przeciwników", "wykonuje 3 mocne ciêcia", 0.5f, 3, 1.75f, 0.9f, 0, false, false, true, false, statusEffects);

    public SwordMayhem()
    {
        Name = "Sza³ szabli";
        SkillDescription = "wykonuje 3 mocne ciêcia w losowych przeciwników.";
        InFightDescription = " wykonuje 3 mocne ciêcia, zadaj¹c ";
        Cost = 0.5f;
        Repetitions = 3;
        AccuracyMultiplier = 0.9f;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = true;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.NominativeName + " nie trafia szabl¹ w " + target.AccusativeName;
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
