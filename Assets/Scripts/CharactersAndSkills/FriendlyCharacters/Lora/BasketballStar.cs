using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballStar : PlayableSkill
{
    public BasketballStar()
    {
        Name = "Gwiazka koszyk�wki";
        SkillDescription = "rzuca z du�� precyzj� pi�k� w przeciwnika. Zadaje �rednie obra�enia jednemu przeciwnikowi.";
        InFightDescription = " rzuca w przeciwnika za 3 i zadaje mu ";
        Cost = 50;
        AccuracyMultiplier = 1.25f;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = false;
        SkillSoundId = 8;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.NominativeName + " nie trafia " + target.DativeName;
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int damage = (int)(source.Attack * 1.5f * Random.Range(0.8f, 1.2f)) - target.Defense;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            damage *= source.criticalDamageMultiplier;
        }
        finalDesc = finalDesc + damage + " obra�e�!";
        target.TakeDamage(damage);
        return finalDesc;
    }
}
