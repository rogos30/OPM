using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoraleBoost : EnemySkill
{
    public EnemyMoraleBoost() : base()
    {
        Name = "Motywacja";
        InFightDescription = " zagrzewa zesp� do pracy, wzmacniaj�c ";
        TargetIsFriendly = true;
        TargetIsSelf = false;
        MultipleTargets = true;
        SkillSoundId = 27;
    }

    public override string execute(EnemyCharacter source, Character target)
    {
        if (Random.Range(0, 1f) > source.Accuracy * AccuracyMultiplier)
        {
            return source.NominativeName + " przypadkiem zwyzywa� swoich pracownik�w";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName;
        int turns = 3;
        if (Random.Range(0, 1f) < criticalChance)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 5;
        }
        target.ApplyBuff((int)Character.StatusEffects.ATTACK, turns);
        target.ApplyBuff((int)Character.StatusEffects.DEFENSE, turns);
        finalDesc = finalDesc + " atak i obron� na " + (turns - 1) + " tury!";
        return finalDesc;
    }
}
