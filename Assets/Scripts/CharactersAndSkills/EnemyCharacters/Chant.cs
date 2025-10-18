using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chant : EnemySkill
{
    public Chant()
    {
        Name = "Przy�piewka";
        InFightDescription = " czci Legi� Warszaw�, zwi�kszaj�c sobie si�� na ";
        TargetIsFriendly = false;
        TargetIsSelf = true;
        MultipleTargets = false;
        SkillSoundId = 9;
    }

    public override string execute(EnemyCharacter source, Character target)
    {
        if (Random.Range(0, 1f) > source.Accuracy * AccuracyMultiplier)
        {
            return source.NominativeName + " przestaje kibicowa� Legii";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int turns = 3;
        if (Random.Range(0, 1f) < criticalChance)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 5;
        }
        finalDesc = finalDesc + (turns - 1) + " tury!";
        target.ApplyBuff((int)Character.StatusEffects.ATTACK, turns);
        target.ApplyBuff((int)Character.StatusEffects.ATTACK, turns);
        return finalDesc;
    }
}
