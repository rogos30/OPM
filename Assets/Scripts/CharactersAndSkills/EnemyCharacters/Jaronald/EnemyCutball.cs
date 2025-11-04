using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCutball : EnemySkill
{
    public EnemyCutball() : base()
    {
        Name = "Œcina";
        InFightDescription = " œcina pi³eczkê i zadaje ";
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
    }

    public override string execute(EnemyCharacter source, Character target)
    {
        if (Random.Range(0, 1f) > source.Accuracy * AccuracyMultiplier)
        {
            return source.NominativeName + " nie trafia w pi³eczkê";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName;
        int damage = (int)(source.Attack * 2.5f) - target.Defense;
        if (Random.Range(0, 1f) < criticalChance)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            damage *= source.criticalDamageMultiplier;
        }

        damage = Mathf.Max(damage, 1);
        target.TakeDamage(damage);
        if (((FriendlyCharacter)target).IsGuarding && damage != 1)
        {
            damage /= 2;
        }
        finalDesc = finalDesc + " " + damage + " obra¿eñ!";
        return finalDesc;
    }
}
