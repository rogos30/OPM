using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBarrage : PlayableSkill
{
    bool wasUsed = false;
    public ControllerBarrage()
    {
        SetToUsable();
        Level = 0;
        MaxLevel = 2;
        levelsToUpgrades = new List<int> { 10, 10 };
        tokensToUpgrades = new List<int> { 4, 1, };
        upgradeNames = new List<string> { "Odblokuj umiejêtnoœæ " + Name, "Zmniejsz koszt umiejêtnoœci " + Name };
        upgradeDescriptions = new List<string> { "Zadaje spore obra¿enia 6 razy. Mo¿na u¿yc tylko raz w walce", "100% -> 90% maxSP"};
    }
    public void SetToUsable()
    {
        wasUsed = false;
        Name = "Ostrza³ padami";
        SkillDescription = "rzuca 3 padami jak bumerangami. Zadaje spore obra¿enia 6 razy. Mo¿na u¿yæ tylko raz w walce";
        InFightDescription = " rzuca padami i zadaje ";
        Cost = 1f;
        Repetitions = 6;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = true;
        SkillSoundId = 14;

    }

    public void SetToUseless()
    {
        wasUsed = true;
        Name = "Ostrza³ padami (WYKORZYSTANY)";
        SkillDescription = "ju¿ nie mo¿e wykorzystaæ tej umiejêtnoœci";
        InFightDescription = " odpoczywa";
        Cost = 999;
        TargetIsFriendly = false;
        TargetIsSelf = true;
        MultipleTargets = false;
        TargetIsRandom = false;
    }
    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (!wasUsed)
        {
            if (skillPerformance == 0)
            {
                return source.NominativeName + " nie trafia padem w " + target.AccusativeName;
            }
            string finalDesc = source.NominativeName + InFightDescription + target.DativeName + " ";
            int damage = (int)(source.Attack * 1.5f * Random.Range(0.8f, 1.2f)) - target.Defense;
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
        else
        {
            string finalDesc = source.NominativeName + InFightDescription;
            return finalDesc;
        }
    }
    public override void upgrade()
    {
        if (Level == 0)
        {
            IsUnlocked = true;
        }
        if (Level == 1)
        {
            Cost = 0.9f;
        }
        Level++;
    }

}
