using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UkrainianStrength : PlayableSkill
{
    int defaultTurns = 3;
    public UkrainianStrength()
    {
        Name = "Ukraiñska moc";
        SkillDescription = "budzi w sobie moc Swiet³any i przekazuje j¹ sojusznikowi, zwiêkszaj¹c mu atak i celnoœæ.";
        InFightDescription = " przekazuje moc Swiet³any ";
        Cost = 0.4f;
        TargetIsFriendly = true;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = false;
        SkillSoundId = 22;
        MaxLevel = 3;
        levelsToUpgrades = new List<int> { 6, 8, 11 };
        tokensToUpgrades = new List<int> { 2, 2, 2 };
        upgradeNames = new List<string> { "Odblokuj umiejêtnoœæ " + Name, "Zwiêksz czas trwania efektu umiejêtnoœci", "Zmniejsz koszt umiejêtnoœci" };
        upgradeDescriptions = new List<string> { "Zwiêksza atak i celnoœæ sojusznika", "2 -> 3 tury", "40% -> 33% maxSP" };
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.DativeName + " nie udaje siê obudziæ swojej ukraiñskiej mocy";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int turns = defaultTurns;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = defaultTurns + 1;
        }
        finalDesc = finalDesc + " " + (turns-1) + " tury!";
        target.ApplyBuff((int)Character.StatusEffects.ATTACK, turns);
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
            defaultTurns = 4;
        }
        if (Level == 2)
        {
            Cost = 0.33f;
        }
        Level++;
    }
}
