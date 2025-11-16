using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aphrodite : PlayableSkill
{
    public Aphrodite() : base()
    {
        Name = "Afrodyta";
        SkillDescription = "przebiera siê za babê, zniechêcaj¹c przeciwników do mocnych ciosów";
        InFightDescription = " zmniejsza atak ";
        Cost = 0.35f;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = true;
        TargetIsRandom = false;
        AnimationId = 4;
        SkillSoundId = 0;
        MaxLevel = 2;
        levelsToUpgrades = new List<int> { 3, 4 };
        tokensToUpgrades = new List<int> { 1, 1 };
        upgradeNames = new List<string> { "Odblokuj umiejêtnoœæ " + Name, "Zmniejsz koszt umiejêtnoœci"};
        upgradeDescriptions = new List<string> { "Zmniejsza atak przeciwników", "35% -> 25% maxSP" };
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.NominativeName + " rozrywa swoje przebranie i czar pryska";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.AccusativeName;
        int turns = 3;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 4;
        }
        finalDesc = finalDesc + " na " + (turns-1) + " tury!";
        target.ApplyDebuff((int)Character.StatusEffects.ATTACK, turns);
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
            Cost = 0.25f;
        }
        Level++;
    }
}
