using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelStrike : EnemySkill
{
    public ShovelStrike() : base()
    {
        Name = "£omot ³opat¹";
        InFightDescription = " wyprowadza potê¿ny cios ³opat¹, czym zadaje ";
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        SkillSoundId = 26;
    }

    public override string execute(EnemyCharacter source, Character target)
    {
        if (Random.Range(0, 1f) > source.Accuracy * AccuracyMultiplier)
        {
            return source.NominativeName + " nie trafia ³opat¹ w " + target.AccusativeName;
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName;
        int damage = (int)(source.Attack * 1.75f * Random.Range(0.8f, 1.2f)) - target.Defense;
        int turns = 2;
        if (Random.Range(0, 1f) < criticalChance)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 3;
            damage *= source.criticalDamageMultiplier;
        }

        damage = Mathf.Max(damage, 1);
        target.TakeDamage(damage);
        if (((FriendlyCharacter)target).IsGuarding && damage != 1)
        {
            damage /= 2;
        }
        target.ApplyDebuff((int)Character.StatusEffects.TURNS, turns);
        finalDesc = finalDesc + " " + damage + " obra¿eñ i parali¿uje cel na " + (turns - 1) + " tury!";
        return finalDesc;
    }
}
