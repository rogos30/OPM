using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTwirl : PlayableSkill
{
    //skill = new Skill("Wir szabli", "kr�ci si� z szabl� mi�dzy przeciwnikami, ka�demu zadaj�c obra�enia", "rozkr�ca wir szabli", 0.5f, 1, 1.75f, 0.9f, 0, false, false, false, true, statusEffects);

    public SwordTwirl()
    {
        Name = "Wir szabli";
        SkillDescription = "kr�ci si� z szabl� mi�dzy przeciwnikami, ka�demu zadaj�c obra�enia.";
        InFightDescription = " rozkr�ca wir szabli ";
        Cost = 0.5f;
        AccuracyMultiplier = 0.9f;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = true;
        TargetIsRandom = false;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.NominativeName + " nie trafia szabl� w " + target.AccusativeName;
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName;
        int damage = (int)(source.Attack * 1.75f * Random.Range(0.8f, 1.2f)) - target.Defense;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            damage *= source.criticalDamageMultiplier;
        }
        damage = Mathf.Max(damage, 1);
        finalDesc = finalDesc + " " + damage + " obra�e�!";
        target.TakeDamage(damage);
        return finalDesc;
    }
}
