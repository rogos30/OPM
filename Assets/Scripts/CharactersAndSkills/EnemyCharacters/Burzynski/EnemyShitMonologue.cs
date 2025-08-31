using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShitMonologue : EnemySkill
{
    public EnemyShitMonologue() : base()
    {
        Name = "Gówniany monolog";
        InFightDescription = " zmniejsza celnoœæ ";
        AccuracyMultiplier = 0.75f;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = true;
        AnimationId = 4;
        SkillSoundId = 5;
    }

    public override string execute(EnemyCharacter source, Character target)
    {
        if (Random.Range(0, 1f) > source.Accuracy * AccuracyMultiplier)
        {
            return source.DativeName + " nie udaje siê zdemotywowaæ przeciwników.";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName;
        int turns = 3;
        if (Random.Range(0, 1f) < criticalChance)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 5;
        }
        finalDesc = finalDesc + " na " + turns + " tury!";
        target.ApplyDebuff((int)Character.StatusEffects.ACCURACY, turns);
        return finalDesc;
    }
}
