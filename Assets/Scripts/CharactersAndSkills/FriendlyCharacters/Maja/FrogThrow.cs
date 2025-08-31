using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogThrow : PlayableSkill
{
    public FrogThrow() : base()
    {
        Name = "Rzut ¿ab¹";
        SkillDescription = "rzuca w przeciwnika ¿ab¹. Krytyczne trafienie nak³ada truciznê.";
        InFightDescription = " rzuca ¿ab¹ w ";
        Cost = 0.2f;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = false;
        SkillSoundId = 25;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        AnimationId = 1;
        if (skillPerformance == 0)
        {
            return source.NominativeName + " nie trafia ¿ab¹ w " + target.AccusativeName;
        }
        string finalDesc = source.NominativeName + InFightDescription + target.AccusativeName;
        int damage = (int)(source.Attack * 1.67f) - target.Defense;
        if (skillPerformance == 2)
        {
            int turns = 4;
            finalDesc = finalDesc + ", nak³adaj¹c truciznê na " + (turns-1) + " tury";
            target.ApplyDebuff((int)Character.StatusEffects.HEALTH, turns);
            AnimationId = 4;
        }
        damage = Mathf.Max(damage, 1);
        finalDesc = finalDesc + " i zadaje " + damage + " obra¿eñ!";
        target.TakeDamage(damage);
        return finalDesc;
    }
}
