using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKorwinsPistol : EnemySkill
{
    public EnemyKorwinsPistol()
    {
        Name = "Pistolet Korwina";
        InFightDescription = " stawia kres lewactwu i zadaje ";
        Repetitions = 3;
        AnimationId = 7;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        SkillSoundId = 11;
    }

    public override string execute(EnemyCharacter source, Character target)
    {
        if (Random.Range(0, 1f) > source.Accuracy * AccuracyMultiplier)
        {
            source.TakeDamage(source.Attack - source.Defense);
            return source.NominativeName + " trafia sam siebie!";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName;
        int damage = (int)(source.Attack * 1.75f * Random.Range(0.8f, 1.2f)) - target.Defense;
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
