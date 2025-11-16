using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoraleBoost : PlayableSkill
{
    int defaultTurns = 2;

    public MoraleBoost() : base()
    {
        Name = "Podbicie morale";
        SkillDescription = "zwiêksza morale w dru¿ynie, zwiêkszaj¹c celnoœæ sojusznikom";
        InFightDescription = " zwiêksza celnoœæ ";
        Cost = 50;
        TargetIsFriendly = true;
        TargetIsSelf = false;
        MultipleTargets = true;
        TargetIsRandom = false;
        SkillSoundId = 17;
        MaxLevel = 2;
        levelsToUpgrades = new List<int> { 4, 5 };
        tokensToUpgrades = new List<int> { 1, 1 };
        upgradeNames = new List<string> { "Odblokuj umiejêtnoœæ " + Name, "Wyd³u¿ czas trwania umiejêtnoœci"};
        upgradeDescriptions = new List<string> { "Zwiêksza celnoœæ dru¿yny na kilka tur", "1 -> 2 tury" };
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.DativeName + " nie udaje siê pocieszyæ dru¿yny";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName;
        int turns = defaultTurns;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = defaultTurns + 2;
        }
        finalDesc = finalDesc + " na " + (turns - 1) + " tury!";
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
            defaultTurns = 3;
        }
        Level++;
    }
}
