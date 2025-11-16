using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoraleDebuff : PlayableSkill
{
    int defaultTurns = 3;
    public MoraleDebuff() : base()
    {
        Name = "Przemowa do ludu";
        SkillDescription = "jako Cesarz Kibla bierze siê za demotywacjê przeciwników";
        InFightDescription = " zmniejsza celnoœæ ";
        Cost = 110;
        AccuracyMultiplier = 0.75f;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = true;
        TargetIsRandom = false;
        AnimationId = 4;
        SkillSoundId = 5;
        MaxLevel = 3;
        levelsToUpgrades = new List<int> { 1, 1, 12 };
        tokensToUpgrades = new List<int> { 2, 1, 2 };
        upgradeNames = new List<string> { "Odblokuj umiejêtnoœæ " + Name, "Wyd³u¿ czas trwania efektu umiejêtnoœci" };
        upgradeDescriptions = new List<string> { "Zmniejsza celnoœæ wszystkim przeciwnikom", "2 -> 3 tury"};
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.DativeName + "nie udaje siê zdemotywowaæ przeciwników.";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName;
        int turns = defaultTurns;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = defaultTurns + 2;
        }
        finalDesc = finalDesc + " na " + (turns-1) + " tury!";
        target.ApplyDebuff((int)Character.StatusEffects.ACCURACY, turns);
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
            defaultTurns = 4;
        }
        Level++;
    }
}
