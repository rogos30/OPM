using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : EnemySkill
{
    public Grunt() : base()
    {
        Name = "Chrumkniêcie";
        InFightDescription = " chrumka tak mocno, ¿e zadaje ";
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = true;
        AccuracyMultiplier = 0.8f;
    }

    public override string execute(EnemyCharacter source, Character target)
    {
        if (Random.Range(0, 1f) > source.Accuracy * AccuracyMultiplier)
        {
            return source.NominativeName + " nie chrumka na " + target.AccusativeName;
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName;
        int damage = (int)(source.Attack * Random.Range(0.8f, 1.2f)) - target.Defense;
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
