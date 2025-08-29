using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idiocy : EnemySkill
{
    public Idiocy() : base()
    {
        Name = "Idiotyzm";
        InFightDescription = " gada takie g�upoty, �e ";
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
            return source.DativeName + " nie powiedzia�a " + target.DativeName + " nic g�upiego (nie odezwa�a si�)";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.NominativeName + " traci troch� IQ, obrony i celno�ci na ";
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
