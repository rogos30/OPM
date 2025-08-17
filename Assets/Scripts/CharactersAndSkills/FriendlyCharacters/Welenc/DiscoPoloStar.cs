using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoPoloStar : PlayableSkill
{
    public DiscoPoloStar()
    {
        Name = "Gwiazda disco polo";
        SkillDescription = "parodiuje Zenka i wszyscy dobrze siê bawi¹. Wzmacnia na krótko ca³¹ dru¿ynê.";
        InFightDescription = " muzycznie zwiêksza statystyki ";
        Cost = 200;
        TargetIsFriendly = true;
        TargetIsSelf = false;
        MultipleTargets = true;
        TargetIsRandom = false;
        SkillSoundId = 6;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        int turns;
        if (skillPerformance == 0)
        {
            turns = 2;
            target.ApplyDebuff((int)Character.StatusEffects.ATTACK, turns);
            return source.NominativeName + " fatalnie fa³szuje, zmniejszaj¹c atak " + target.AccusativeName + "na 1 turê";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.AccusativeName;
        turns = 2;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 3;
        }
        finalDesc = finalDesc + " na " + (turns-1) + " tury!";
        target.ApplyBuff((int)Character.StatusEffects.ATTACK, turns);
        target.ApplyBuff((int)Character.StatusEffects.DEFENSE, turns);
        target.ApplyBuff((int)Character.StatusEffects.ACCURACY, turns);
        //((Welenc)source).IncreaseAttackMultiplier();
        return finalDesc;
    }
}
