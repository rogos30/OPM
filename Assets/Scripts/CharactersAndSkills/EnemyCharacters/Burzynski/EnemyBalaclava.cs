using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBalaclava : EnemySkill
{
    public EnemyBalaclava()
    {
        Name = "Kominiarka";
        InFightDescription = " zak³ada kominiarkê i zwiêksza sobie obronê na ";
        TargetIsFriendly = false;
        TargetIsSelf = true;
        MultipleTargets = false;
        SkillSoundId = 10;
    }

    public override string execute(EnemyCharacter source, Character target)
    {
        if (Random.Range(0, 1f) > source.Accuracy * AccuracyMultiplier)
        {
            return source.DativeName + " nie udaje siê za³o¿yæ kominiarki.";
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
        return finalDesc;
    }
}
