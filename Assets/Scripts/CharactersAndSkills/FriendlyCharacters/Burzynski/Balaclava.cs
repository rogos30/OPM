using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balaclava : PlayableSkill
{
    int defaultTurns = 3;
    public Balaclava()
    {
        Name = "Kominiarka";
        SkillDescription = "zak³ada kominiarkê, zwiêkszaj¹c sobie obronê.";
        InFightDescription = " zak³ada kominiarkê i zwiêksza sobie obronê na ";
        Cost = 50;
        TargetIsFriendly = false;
        TargetIsSelf = true;
        MultipleTargets = false;
        TargetIsRandom = false;
        SkillSoundId = 10;
        MaxLevel = 2;
        levelsToUpgrades = new List<int> { 1, 1 };
        tokensToUpgrades = new List<int> { 1, 1 };
        upgradeNames = new List<string> { "Odblokuj umiejêtnoœæ " + Name, "Wyd³u¿ czas trwania efektu umiejêtnoœci" };
        upgradeDescriptions = new List<string> { "Zwiêksza sobie obronê na kilka tur", "2 -> 3 tury" };
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.DativeName + " nie udaje siê za³o¿yæ kominiarki.";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int turns = defaultTurns;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = defaultTurns + 2;
        }
        finalDesc = finalDesc + (turns-1) + " tury!";
        target.ApplyBuff((int)Character.StatusEffects.DEFENSE, turns);
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
