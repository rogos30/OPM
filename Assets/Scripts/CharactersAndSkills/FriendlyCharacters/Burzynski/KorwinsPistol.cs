using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KorwinsPistol : PlayableSkill
{
    public KorwinsPistol()
    {
        Name = "Pistolet Krula";
        SkillDescription = "pos³uguje siê broni¹ Krula w celu rozprawienia siê z komunistami.";
        InFightDescription = " stawia kres lewactwu i zadaje ";
        Cost = 240;
        Repetitions = 3;
        AccuracyMultiplier = 1f;
        AnimationId = 7;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = true;
        SkillSoundId = 11;
        MaxLevel = 3;
        levelsToUpgrades = new List<int> { 1, 1, 12 };
        tokensToUpgrades = new List<int> { 3, 1, 3 };
        upgradeNames = new List<string> { "Odblokuj umiejêtnoœæ " + Name, "Zwiêksz celnoœæ umiejêtnoœci", "Strzel wiêksz¹ iloœci¹ pocisków" };
        upgradeDescriptions = new List<string> { "Zadaje spore obra¿enia kilka razy losowym przeciwnikom", "+10% celnoœci", "3 -> 4 strza³y" };
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            source.TakeDamage(source.Attack - source.Defense);
            return source.NominativeName + " trafia sam siebie!";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName;
        int damage = (int)(source.Attack * 1.75f * Random.Range(0.8f, 1.2f)) - target.Defense;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            damage *= source.criticalDamageMultiplier;
        }
        damage = Mathf.Max(damage, 1);
        finalDesc = finalDesc + " " + damage + " obra¿eñ!";
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
            AccuracyMultiplier = 1.1f;
        }
        if (Level == 2)
        {
            Repetitions = 4;
        }
        Level++;
    }
}
