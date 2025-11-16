using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoPoloStar : PlayableSkill
{
    bool missDebuffAllies = true;
    public DiscoPoloStar() : base()
    {
        Name = "Gwiazda disco polo";
        SkillDescription = "parodiuje Zenka i wszyscy dobrze siê bawi¹. Wzmacnia na krótko ca³¹ dru¿ynê.";
        InFightDescription = " muzycznie zwiêksza statystyki ";
        Cost = 250;
        TargetIsFriendly = true;
        TargetIsSelf = false;
        MultipleTargets = true;
        TargetIsRandom = false;
        SkillSoundId = 7;
        MaxLevel = 3;
        levelsToUpgrades = new List<int> { 6, 6, 8 };
        tokensToUpgrades = new List<int> { 2, 1, 1 };
        upgradeNames = new List<string> { "Odblokuj umiejêtnoœæ " + Name, "Zmniejsz koszt umiejêtnoœci " + Name, "Neguj efekt spud³owania umiejêtnoœci¹ " + Name };
        upgradeDescriptions = new List<string> { "Wzmacnia na krótko ca³¹ dru¿ynê", "250 -> 200 SP", "Pud³o ju¿ nie zmniejsza celnoœci towarzyszy" };
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        int turns;
        if (skillPerformance == 0)
        {
            if (missDebuffAllies)
            {
                turns = 2;
                target.ApplyDebuff((int)Character.StatusEffects.ATTACK, turns);
                return source.NominativeName + " fatalnie fa³szuje, zmniejszaj¹c atak " + target.AccusativeName + "na 1 turê";
            }
            return source.NominativeName + " fatalnie fa³szuje";
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
        return finalDesc;
    }
    public override void upgrade()
    {
        if (Level == 0)
        {
            IsUnlocked = true;
        }
        if (Level == 1)
        {
            Cost = 200;
        }
        if (Level == 2)
        {
            missDebuffAllies = false;
        }
        Level++;
    }
}
