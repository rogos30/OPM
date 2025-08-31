using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDinology : EnemySkill
{
    public EnemyDinology()
    {
        Name = "Dinologia";
        InFightDescription = " opowiada o dinozaurach ";
        AccuracyMultiplier = 0.5f;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        AnimationId = 4;
        SkillSoundId = 4;
    }

    public override string execute(EnemyCharacter source, Character target)
    {
        if (Random.Range(0, 1f) > source.Accuracy * AccuracyMultiplier)
        {
            return source.NominativeName + " nie zaciekawia " + target.AccusativeName + " (nic dziwnego)";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName;
        int turns = 3;
        if (Random.Range(0, 1f) < criticalChance)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 4;
        }
        finalDesc = finalDesc + " i parali¿uje na " +  (turns-1) + " tury!";
        target.ApplyDebuff((int)Character.StatusEffects.TURNS, turns);
        return finalDesc;
    }
}
