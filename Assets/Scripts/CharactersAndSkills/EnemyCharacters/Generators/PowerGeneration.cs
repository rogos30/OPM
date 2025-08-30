using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGeneration : EnemySkill
{
    public PowerGeneration() : base()
    {
        Name = "Generacja pr�du";
        InFightDescription = " generuje pr�d, wzmacniaj�c sobie atak na ";
        TargetIsFriendly = false;
        TargetIsSelf = true;
        MultipleTargets = false;
        SkillSoundId = 31;
    }

    public override string execute(EnemyCharacter source, Character target)
    {
        if (Random.Range(0, 1f) > source.Accuracy * AccuracyMultiplier)
        {
            return source.DativeName + " nie udaje si� wygenerowa� pr�du";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int healing = 500;
        int turns = 3;
        if (Random.Range(0, 1f) < criticalChance)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            healing *= source.criticalDamageMultiplier;
            turns = 5;
        }
        target.Heal(healing);
        target.ApplyBuff((int)Character.StatusEffects.ATTACK, turns);
        finalDesc = finalDesc + (turns-1) + " tury i odnawiaj�c " + healing + " zdrowia!";
        return finalDesc;
    }
}
