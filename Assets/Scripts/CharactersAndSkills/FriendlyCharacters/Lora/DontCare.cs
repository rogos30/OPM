using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontCare : PlayableSkill
{
    public DontCare()
    {
        Name = "Wywalone";
        SkillDescription = "ma gdzieœ, co siê z ni¹ stanie. Nak³ada sobie buff na losow¹ statystykê.";
        InFightDescription = " trafi³a wzmocnienie ";
        Cost = 65;
        TargetIsFriendly = false;
        TargetIsSelf = true;
        MultipleTargets = false;
        TargetIsRandom = false;
        SkillSoundId = 24;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.NominativeName + " jednak ma pecha.";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int turns = 3;
        int effect = Random.Range(0, 4);
        switch (effect)
        {
            case 0:
                finalDesc = finalDesc + "ataku";
                break;
            case 1:
                finalDesc = finalDesc + "obrony";
                break;
            case 2:
                finalDesc = finalDesc + "celnoœci";
                break;
            case 3:
                finalDesc = finalDesc + "regeneracji";
                break;
        }
        target.ApplyBuff(effect, turns);
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc + " i ";
            int additionalEffect;
            do
            {
                additionalEffect = Random.Range(0, 4);
            } while (additionalEffect == effect);
            switch (additionalEffect)
            {
                case 0:
                    finalDesc = finalDesc + "ataku";
                    break;
                case 1:
                    finalDesc = finalDesc + "obrony";
                    break;
                case 2:
                    finalDesc = finalDesc + "celnoœci";
                    break;
                case 3:
                    finalDesc = finalDesc + "regeneracji";
                    break;
            }
            target.ApplyBuff(additionalEffect, turns);
        }
        finalDesc = finalDesc + " na " + (turns - 1) + " tury!";
        return finalDesc;
    }
}
