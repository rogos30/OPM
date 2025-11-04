using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheats : EnemySkill
{
    public EnemyCheats()
    {
        Name = "Cheaty";
        InFightDescription = " wpisuje cheaty i daje sobie dodatkowy ruch na ";
        TargetIsFriendly = false;
        TargetIsSelf = true;
        MultipleTargets = false;
        SkillSoundId = 3;
    }

    public override string execute(EnemyCharacter source, Character target)
    {
        if (Random.Range(0, 1f) > source.Accuracy * AccuracyMultiplier)
        {
            return source.DativeName + " wpisuje niedzia³aj¹ce kody";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int turns = 3;
        if (Random.Range(0, 1f) < criticalChance)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 4;
        }
        finalDesc = finalDesc + (turns - 1) + " tury!";
        target.ApplyBuff((int)Character.StatusEffects.TURNS, turns);
        return finalDesc;
    }
}
