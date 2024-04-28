using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectionPromise : PlayableSkill
{
    //skill = new Skill("Obietnica wyborcza", "spe³nia swoj¹ obietnicê wyborcz¹", "nie robi nic", 0, 1, 0, 1, 0, false, true, false, false, statusEffects);

    public ElectionPromise()
    {
        Name = "Obietnica wyborcza";
        SkillDescription = "spe³nia swoj¹ obietnicê wyborcz¹.";
        InFightDescription = "";
        Cost = 1;
        TargetIsFriendly = true;
        TargetIsSelf = false;
        MultipleTargets = true;
        TargetIsRandom = false;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 1 || skillPerformance == 2)
        {
            return source.NominativeName + " nie robi nic";
        }
        float criticalChance = 0.1f;
        string finalDesc = source.AccusativeName + " POJEBA£O! ";
        if (target.KnockedOut)
        {
            finalDesc += "Cuci i ";
            target.KnockedOut = false;
            target.Health = target.MaxHealth / 2;
        }
        finalDesc += "wali bombê " + target.DativeName + " za ";
        int damage = (int)(source.Attack * Random.Range(0.8f, 1.2f)) - target.Defense;
        if (((FriendlyCharacter)target).IsGuarding)
        {
            damage = (int)(damage * FriendlyCharacter.guardDamageMultiplier);
        }
        if (Random.Range(0,1f) < criticalChance)
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
