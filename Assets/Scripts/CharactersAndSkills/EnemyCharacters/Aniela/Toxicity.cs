using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toxicisity : EnemySkill
{
    public Toxicisity() : base()
    {
        Name = "Toksycznoœæ";
        InFightDescription = " wykazuje tak¹ toksycznoœæ, ¿e nak³ada truciznê na ";
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        AccuracyMultiplier = 0.8f;
        AnimationId = 4;
        SkillSoundId = 39;
    }

    public override string execute(EnemyCharacter source, Character target)
    {
        if (Random.Range(0, 1f) > source.Accuracy * AccuracyMultiplier)
        {
            return source.NominativeName + " zaskakuj¹co nie wykazuje toksycznoœci wobec " + target.AccusativeName;
        }
        string finalDesc = source.NominativeName + InFightDescription + target.AccusativeName + " na ";
        int turns = 4;
        if (Random.Range(0, 1f) < criticalChance)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 6;
            finalDesc = finalDesc + " " + (turns - 1) + " tur!";
        }
        else
        {
            finalDesc = finalDesc + " " + (turns - 1) + " tury!";
        }

        target.ApplyDebuff((int)Character.StatusEffects.HEALTH, turns);
        return finalDesc;
    }
}
