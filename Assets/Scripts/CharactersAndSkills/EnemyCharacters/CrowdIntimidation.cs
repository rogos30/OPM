using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdIntimidation : EnemySkill
{
    public CrowdIntimidation() : base()
    {
        Name = "Zastraszanie od t�umu";
        InFightDescription = " motywuje t�um do zastraszania przeciwnik�w ";
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = true;
        SkillSoundId = 41;
    }

    public override string execute(EnemyCharacter source, Character target)
    {
        if (Random.Range(0, 1f) > source.Accuracy * AccuracyMultiplier)
        {
            return source.NominativeName + " nie wykorzystuje t�umu";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName + ", zadaj�c ";
        int damage = (int)(source.Attack * Random.Range(0.8f, 1.2f)) - target.Defense;
        int turns = 3;
        if (Random.Range(0, 1f) < criticalChance)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 4;
            damage *= source.criticalDamageMultiplier;
        }
        target.ApplyDebuff((int)Character.StatusEffects.DEFENSE, turns);
        damage = Mathf.Max(damage, 1);
        target.TakeDamage(damage);
        if (((FriendlyCharacter)target).IsGuarding && damage != 1)
        {
            damage /= 2;
        }
        finalDesc = finalDesc + damage + " obra�e� i zmniejszaj�c atak i obron� na " + (turns - 1) + " tury!";
        return finalDesc;
    }
}
