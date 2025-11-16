using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinology : PlayableSkill
{
    public Dinology()
    {
        Name = "Dinologia";
        SkillDescription = "zanudza przeciwnika ciekawostkami o dinozaurach. Ciê¿ko trafiæ, ale parali¿uje.";
        InFightDescription = " opowiada o dinozaurach ";
        Cost = 0.35f;
        AccuracyMultiplier = 0.5f;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = false;
        AnimationId = 4;
        SkillSoundId = 4;
        MaxLevel = 2;
        levelsToUpgrades = new List<int> { 1, 1 };
        tokensToUpgrades = new List<int> { 2, 1 };
        upgradeNames = new List<string> { "Odblokuj umiejêtnoœæ " + Name, "Zmniejsz koszt umiejêtnoœci" };
        upgradeDescriptions = new List<string> { "Parali¿uje wybrany cel, ale ciê¿ko trafiæ", "35% -> 30% maxSP" };
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.DativeName + " nie zaciekawia dinozaurami " + target.AccusativeName + " (nic dziwnego)";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName;
        int turns = 3;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 4;
        }
        finalDesc = finalDesc + " i parali¿uje na " +  (turns-1) + " tury!";
        target.ApplyDebuff((int)Character.StatusEffects.TURNS, turns);
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
        Level++;
    }
}
