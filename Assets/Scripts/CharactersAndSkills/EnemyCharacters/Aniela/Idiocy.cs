using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idiocy : EnemySkill
{
    public Idiocy() : base()
    {
        Name = "Idiotyzm";
        InFightDescription = " gada takie g³upoty, ¿e ";
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        AnimationId = 4;
        SkillSoundId = 38;
    }

    public override string execute(EnemyCharacter source, Character target)
    {
        if (Random.Range(0, 1f) > source.Accuracy * AccuracyMultiplier)
        {
            return source.DativeName + " nie powiedzia³a " + target.DativeName + " nic g³upiego (nie odezwa³a siê)";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.NominativeName + " traci trochê IQ, obrony i celnoœci na ";
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
        target.ApplyDebuff((int)Character.StatusEffects.ACCURACY, turns);
        target.ApplyDebuff((int)Character.StatusEffects.DEFENSE, turns);
        return finalDesc;
    }
}
