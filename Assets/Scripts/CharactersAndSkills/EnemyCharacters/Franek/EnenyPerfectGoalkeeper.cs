using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPerfectGoalkeeper : EnemySkill
{
    //* Perfekcyjny bramkarz – Je¿eli ma na³o¿ony debuff na obronê, to go zdejmuje. Nastêpnie zwiêksza swoj¹ obronê na 3 tury.

    public EnemyPerfectGoalkeeper()
    {
        Name = "Super bramkarz";
        InFightDescription = " przygotowuje siê na strza³y przeciwnika na ";
        TargetIsFriendly = false;
        TargetIsSelf = true;
        MultipleTargets = false;
        SkillSoundId = 42;
    }

    public override string execute(EnemyCharacter source, Character target)
    {
        if (Random.Range(0, 1f) > source.Accuracy * AccuracyMultiplier)
        {
            return source.NominativeName + " wpuszcza banalnego gola";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int turns = 3;
        if (Random.Range(0, 1f) < criticalChance)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 5;
        }
        finalDesc = finalDesc + (turns - 1) + " tury!";
        target.ApplyBuff((int)Character.StatusEffects.DEFENSE, turns);
        target.ApplyBuff((int)Character.StatusEffects.DEFENSE, turns);
        return finalDesc;
    }
}
