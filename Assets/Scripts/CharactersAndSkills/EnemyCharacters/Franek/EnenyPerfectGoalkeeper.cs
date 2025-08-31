using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPerfectGoalkeeper : EnemySkill
{
    //* Perfekcyjny bramkarz � Je�eli ma na�o�ony debuff na obron�, to go zdejmuje. Nast�pnie zwi�ksza swoj� obron� na 3 tury.

    public EnemyPerfectGoalkeeper()
    {
        Name = "Super bramkarz";
        InFightDescription = " przygotowuje si� na strza�y przeciwnika na ";
        TargetIsFriendly = false;
        TargetIsSelf = true;
        MultipleTargets = false;
        SkillSoundId = 9;
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
