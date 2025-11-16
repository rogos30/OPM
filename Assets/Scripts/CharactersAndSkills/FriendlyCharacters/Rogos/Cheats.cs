using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : PlayableSkill
{
    int defaultTurns = 3;

    public Cheats()
    {
        Name = "Cheaty";
        SkillDescription = "oszukuje i daje sobie dodatkowy ruch w turze na kilka tur.";
        InFightDescription = " wpisuje cheaty i daje sobie dodatkowy ruch na ";
        Cost = 0.3f;
        TargetIsFriendly = false;
        TargetIsSelf = true;
        MultipleTargets = false;
        TargetIsRandom = false;
        SkillSoundId = 3;
        MaxLevel = 3;
        levelsToUpgrades = new List<int> { 4, 6, 8};
        tokensToUpgrades = new List<int> { 1, 1, 2 };
        upgradeNames = new List<string> { "Odblokuj umiejêtnoœæ " + Name, "Zmniejsz koszt umiejêtnoœci", "Wyd³u¿ czas trwania umiejêtnoœci" };
        upgradeDescriptions = new List<string> { "Daje dodatkowy ruch w turze na kilka tur", "40% -> 30% max SP", "2 -> 3 tury" };
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.NominativeName + " wpisuje niedzia³aj¹ce kody";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int turns = defaultTurns;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = defaultTurns + 2;
            finalDesc = finalDesc + (turns - 1) + " tur!";
        }
        else
        {
            finalDesc = finalDesc + (turns - 1) + " tury!";
        }
        target.ApplyBuff((int)Character.StatusEffects.TURNS, turns);
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
            Cost = 0.3f;
        }
        if (Level == 2)
        {
            defaultTurns = 4;
        }
        Level++;
    }
}
