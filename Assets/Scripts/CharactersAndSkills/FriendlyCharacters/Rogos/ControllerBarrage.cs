using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBarrage : PlayableSkill
{
    public ControllerBarrage()
    {
        Name = "Ostrza³ padami";
        SkillDescription = "rzuca 3 padami jak bumerangami. Zadaje spore obra¿enia 6 razy";
        InFightDescription = " rzuca padami i zadaje ";
        Cost = 280;
        Repetitions = 6;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = true;
    }
    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.NominativeName + " nie trafia padem w " + target.AccusativeName;
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName;
        int damage = (int)(source.Attack * 1.5f * Random.Range(0.8f, 1.2f)) - target.Defense;
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
