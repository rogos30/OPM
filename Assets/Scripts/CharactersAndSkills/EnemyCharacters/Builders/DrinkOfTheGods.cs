using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkOfTheGods : EnemySkill
{
    public DrinkOfTheGods() : base()
    {
        Name = "Napój bogów";
        InFightDescription = " pije dobrze sch³odzon¹ substancjê, lecz¹c sobie ";
        TargetIsFriendly = false;
        TargetIsSelf = true;
        MultipleTargets = false;
        SkillSoundId = 28;
    }

    public override string execute(EnemyCharacter source, Character target)
    {
        if (Random.Range(0, 1f) > source.Accuracy * AccuracyMultiplier)
        {
            return source.NominativeName + " rozlewa wódeczkê";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName + " ";
        int healing = (int)(source.MaxHealth / 15 * Random.Range(0.8f, 1.2f));
        int turns = 3;
        if (Random.Range(0, 1f) < criticalChance)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 4;
            healing *= source.criticalDamageMultiplier;
        }
        target.ApplyBuff((int)Character.StatusEffects.HEALTH, turns);
        target.ApplyBuff((int)Character.StatusEffects.ATTACK, turns);
        target.ApplyBuff((int)Character.StatusEffects.DEFENSE, turns);
        healing = Mathf.Max(healing, 1);
        target.Heal(healing);
        finalDesc = finalDesc + healing + " zdrowia oraz nak³adaj¹c regeneracjê i wzmacniaj¹c swój atak i obronê na " + (turns - 1) + " tury!";
        return finalDesc;
    }
}
