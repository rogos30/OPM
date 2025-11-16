using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutball : PlayableSkill
{
    float attackMultiplier = 2.5f;
    public Cutball()
    {
        Name = "Œcina";
        SkillDescription = "œcina pi³eczkê w przeciwnika. Wysokie obra¿enia, ale ciê¿ko trafiæ.";
        InFightDescription = " œcina pi³eczkê i zadaje przeciwnikowi ";
        Cost = 150;
        AccuracyMultiplier = 0.4f;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = false;
        SkillSoundId = 16;

        MaxLevel = 3;
        levelsToUpgrades = new List<int> { 6, 10, 15 };
        tokensToUpgrades = new List<int> { 3, 2, 2 };
        upgradeNames = new List<string> { "Odblokuj umiejêtnoœæ " + Name, "Zwiêksz obra¿enia umiejêtnoœci", "Zwiêksz celnoœæ umiejêtnoœci"};
        upgradeDescriptions = new List<string> { "Zadaje bardzo wysokie obra¿enia, ale ciê¿ko trafiæ", "+20% obra¿eñ", "+25% celnoœci" };
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.NominativeName + " nie trafia w pi³eczkê";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int damage = (int)(source.Attack * attackMultiplier * Random.Range(0.8f, 1.2f)) - target.Defense;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            damage *= source.criticalDamageMultiplier;
        }
        damage = Mathf.Max(damage, 1);
        finalDesc = finalDesc + damage + " obra¿eñ!";
        target.TakeDamage(damage);
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
            attackMultiplier = 3;
        }
        if (Level == 2)
        {
            AccuracyMultiplier = 0.5f;
        }
        Level++;
    }
}
