using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlcoholOverdose : EnemySkill
{
    public AlcoholOverdose() : base()
    {
        Name = "Przechlanie";
        InFightDescription = " rzyga na ";
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        AccuracyMultiplier = 1f;
        AnimationId = 4;
    }

    public override string execute(EnemyCharacter source, Character target)
    {
        if (Random.Range(0, 1f) > source.Accuracy * AccuracyMultiplier)
        {
            return source.NominativeName + " nie trafia rzygiem w " + target.AccusativeName;
        }
        string finalDesc = source.NominativeName + InFightDescription + target.AccusativeName + ", nak³adaj¹c truciznê na ";
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
