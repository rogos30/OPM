using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublePunch : EnemySkill
{
    public DoublePunch()
    {
        Name = "Podwójny atak";
        InFightDescription = "wyprowadza 2 zwyk³e ciosy ";
        Repetitions = 2;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
    }
    public override string execute(EnemyCharacter source, Character target)
    {
        if (Random.Range(0, 1f) < source.Accuracy * AccuracyMultiplier)
        {
            return source.NominativeName + "nie trafia atakiem w " + target.AccusativeName;
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName;
        int damage = source.Attack - target.Defense;
        if (Random.Range(0, 1f) < criticalChance)
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
