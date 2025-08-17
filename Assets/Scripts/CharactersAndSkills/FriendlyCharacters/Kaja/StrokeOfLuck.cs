using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrokeOfLuck : PlayableSkill
{
    //skill = new Skill("£ut szczêœcia", "leczy losowego kompana za ca³e jego hp", "odnawia ca³e zdrowie", 175, 1, 0, 0.75f, 1, true, false, true, false, statusEffects);

    public StrokeOfLuck()
    {
        Name = "£ut szczêœcia";
        SkillDescription = "przywraca wybranemu kompanowi losow¹ czêœæ jego hp oraz buffuje losow¹ statystykê.";
        InFightDescription = " przywraca ";
        Cost = 175;
        AccuracyMultiplier = 0.75f;
        TargetIsFriendly = true;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = false;
        SkillSoundId = 8;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.NominativeName + " jednak ma pecha.";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName;
        int turns = 3;
        int effect = UnityEngine.Random.Range(0, 5);
        float healing = UnityEngine.Random.Range(0.25f, 0.6f);
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 5;
            healing = Math.Min(healing * 2, 1);
        }
        finalDesc = finalDesc + " " + healing * 100 + "% max hp i wzmacnia ";
        switch (effect)
        {
            case 0:
                finalDesc = finalDesc + "atak!";
                break;
            case 1:
                finalDesc = finalDesc + "obronê!";
                break;
            case 2:
                finalDesc = finalDesc + "celnoœæ!";
                break;
            case 3:
                finalDesc = finalDesc + "regeneracjê!";
                break;
            case 4:
                finalDesc = finalDesc + "iloœæ tur!";
                break;
        }
        target.ApplyBuff(effect, turns);
        target.Heal(healing);
        return finalDesc;
    }
}
