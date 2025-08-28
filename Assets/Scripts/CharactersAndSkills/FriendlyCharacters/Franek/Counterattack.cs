using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counterattack : PlayableSkill
{
    //* Kontra – Rzuca z du¿¹ si³¹ pi³k¹ w przeciwnika. Zadaje wysokie obra¿enia jednemu przeciwnikowi.

    public Counterattack()
    {
        Name = "Kontra";
        SkillDescription = "rzuca z du¿¹ si³¹ pi³k¹ w przeciwnika. Zadaje wysokie obra¿enia jednemu przeciwnikowi.";
        InFightDescription = " rzuca przeciwnikowi pi³k¹ w ³eb i zadaje mu ";
        Cost = 100;
        AccuracyMultiplier = 0.75f;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = false;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.DativeName + " nie wychodzi kontra";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int damage = (int)(source.Attack * 2.5f * Random.Range(0.8f, 1.2f)) - target.Defense;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            damage *= source.criticalDamageMultiplier;
        }
        finalDesc = finalDesc + damage + " obra¿eñ!";
        target.TakeDamage(damage);
        return finalDesc;
    }
}
