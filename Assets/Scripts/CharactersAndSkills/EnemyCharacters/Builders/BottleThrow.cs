using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleThrow : EnemySkill
{
    public BottleThrow() : base()
    {
        Name = "Rzut butelk¹";
        InFightDescription = " rzuca butelk¹ o ziemiê! Od³amki szk³a trafiaj¹ ";
        Repetitions = 3;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        SkillSoundId = 29;
    }

    public override string execute(EnemyCharacter source, Character target)
    {
        if (Random.Range(0, 1f) > source.Accuracy * AccuracyMultiplier)
        {
            return source.NominativeName + " rzuca butelk¹ o ziemiê, ale butelka nie pêka!";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.AccusativeName + ", zadaj¹c ";
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
        finalDesc = finalDesc + damage + " obra¿eñ!";
        return finalDesc;
    }
}
