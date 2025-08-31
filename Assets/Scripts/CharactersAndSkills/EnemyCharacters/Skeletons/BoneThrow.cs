using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneThrow : EnemySkill
{
    public BoneThrow() : base()
    {
        Name = "Rzut ko�ci�";
        InFightDescription = " �amie sobie ko�� na kawa�ki i rzuc� ni� w przeciwnik�w, zadaj�c sobie ";
        Repetitions = 4;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        SkillSoundId = 29;
        AccuracyMultiplier = 0.8f;
    }

    public override string execute(EnemyCharacter source, Character target)
    {
        int selfDamage = (int)Random.Range(source.MaxHealth * 0.01f * 0.8f, source.MaxHealth * 0.01f * 1.2f);
        source.TakeDamage(selfDamage);
        if (Random.Range(0, 1f) > source.Accuracy * AccuracyMultiplier)
        {
            return source.NominativeName + " �amie sobie ko�� na kawa�ki, zadaj�c sobie " + selfDamage + " obra�e�, ale ni� nie trafia";
        }
        int damage = (int)(source.Attack * 1.2f * Random.Range(0.8f, 1.2f)) - target.Defense;
        string finalDesc = source.NominativeName + InFightDescription + selfDamage + ", a " + target.DativeName + " ";
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
        finalDesc = finalDesc + damage + " obra�e�!";
        return finalDesc;
    }
}
