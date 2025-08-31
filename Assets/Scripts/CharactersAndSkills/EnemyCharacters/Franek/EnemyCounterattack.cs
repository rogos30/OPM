using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounterattack : EnemySkill
{
    //* Kontra – Rzuca z du¿¹ si³¹ pi³k¹ w przeciwnika. Zadaje wysokie obra¿enia jednemu przeciwnikowi.

    public EnemyCounterattack()
    {
        Name = "Kontra";
        InFightDescription = " rzuca przeciwnikowi pi³k¹ w ³eb i zadaje ";
        AccuracyMultiplier = 0.75f;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
    }

    public override string execute(EnemyCharacter source, Character target)
    {
        if (Random.Range(0, 1f) > source.Accuracy * AccuracyMultiplier)
        {
            return source.DativeName + " nie wychodzi kontra";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName + " ";
        int damage = (int)(source.Attack * 2.5f * Random.Range(0.8f, 1.2f)) - target.Defense;
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
