using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LushBangs : PlayableSkill
{
    int defaultTurns = 3;
    public LushBangs()
    {
        Name = "Bujna grzywa";
        SkillDescription = "zarzuca grzyw¹ i zwiêksza poczucie w³asnej wartoœci (i tym samym atak).";
        InFightDescription = " zarzuca grzyw¹ i zwiêksza sobie atak na ";
        Cost = 0.3f;
        TargetIsFriendly = false;
        TargetIsSelf = true;
        MultipleTargets = false;
        TargetIsRandom = false;
        SkillSoundId = 1;
        MaxLevel = 2;
        levelsToUpgrades = new List<int> { 1, 4 };
        tokensToUpgrades = new List<int> { 1, 1 };
        upgradeNames = new List<string> { "Odblokuj umiejêtnoœæ " + Name, "Wyd³u¿ czas dzia³ania umiejêtnoœci"};
        upgradeDescriptions = new List<string> { "Zwiêksza w³asny atak", "2 -> 3 tury" };
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.DativeName + " spada peruka z g³owy";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int turns = defaultTurns;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = defaultTurns + 2;
        }
        finalDesc = finalDesc + (turns-1) + " tur!";
        target.ApplyBuff((int)Character.StatusEffects.ATTACK, turns);
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
