using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doubles : PlayableSkill
{
    int defaultHealing = 500;
    public Doubles()
    {
        Name = "Debel";
        SkillDescription = "gra z kompanem debla. Przywraca sobie i kompanowi 600hp.";
        InFightDescription = " gra z kompanem debla, lecz¹c jemu i sobie ";
        Cost = 0.4f;
        TargetIsFriendly = true;
        TargetIsSelf = true;
        MultipleTargets = false;
        TargetIsRandom = false;
        SkillSoundId = 16;
        MaxLevel = 3;
        levelsToUpgrades = new List<int> { 3, 6, 8 };
        tokensToUpgrades = new List<int> { 2, 1, 1 };
        upgradeNames = new List<string> { "Odblokuj umiejêtnoœæ " + Name, "Zwiêksz leczenie umiejêtnoœci¹", "Zwiêksz leczenie umiejêtnoœci¹" };
        upgradeDescriptions = new List<string> { "Zadaje spore obra¿enia 2 razy", "500 -> 750 HP", "750 -> 1000 HP" };
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.NominativeName + " jednak woli graæ w pojedynkê";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int healing = defaultHealing;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            healing *= source.criticalDamageMultiplier;
        }
        finalDesc = finalDesc + healing + " HP!";
        source.Heal(healing);
        target.Heal(healing);
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
            defaultHealing = 750;
        }
        if (Level == 2)
        {
            defaultHealing = 1000;
        }
        Level++;
    }
}
