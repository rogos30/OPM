using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombelAttack : PlayableSkill
{
    public BombelAttack() : base()
    {
        Name = "Atak";
        SkillDescription = "wyprowadza zwyk³y cios";
        InFightDescription = " wyprowadza zwyk³y cios i zadaje ";
        Cost = 0;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = false;
        SkillSoundId = 26;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        int upgrade = Random.Range(0, 5);
        if (upgrade == 0)
        {
            skillPerformance++;
            Mathf.Min(skillPerformance, 2);
        }
        if (skillPerformance == 0)
        {
            return source.NominativeName + " nie trafia atakiem w " + target.AccusativeName;
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName;
        int damage = (int)(source.Attack * Random.Range(0.8f, 1.2f)) - target.Defense;
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
