using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPL : PlayableSkill
{
    float attackMultiplier = 1.2f;
    bool criticalStuns = false;
    public FPL() : base()
    {
        Name = "FPL";
        SkillDescription = "rozpoczyna dyskusjê o zawodnikach FPL. Zadaje obra¿enia (mentalne) losowemu przeciwnikowi";
        InFightDescription = " zagaduje ";
        Cost = 0.33f;
        AccuracyMultiplier = 0.67f;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = true;
        SkillSoundId = 5;
        MaxLevel = 3;
        levelsToUpgrades = new List<int> { 6, 8, 10 };
        tokensToUpgrades = new List<int> { 1, 2, 2 };
        upgradeNames = new List<string> { "Odblokuj umiejêtnoœæ " + Name, "Zwiêksz obra¿enia i celnoœæ umiejêtnoœci", "Wzmocnij trafienia krytyczne" };
        upgradeDescriptions = new List<string> { "Zmniejsza atak przeciwników", "+25% obra¿eñ, +20% celnoœci", "Trafienia krytyczne parali¿uj¹ cel na 1 turê" };
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        AnimationId = 1;
        if (skillPerformance == 0)
        {
            return target.NominativeName + " ka¿e " + source.DativeName + " siê goniæ";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.AccusativeName;
        int damage = (int)(source.Attack * attackMultiplier * Random.Range(0.8f, 1.2f)) - target.Defense;
        damage = Mathf.Max(damage, 1);
        target.TakeDamage(damage);
        finalDesc = finalDesc + ", zadaj¹c " + damage + " obra¿eñ";
        if (skillPerformance == 2)
        {
            damage *= source.criticalDamageMultiplier;
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            if (criticalStuns)
            {
                int turns = 2;
                target.ApplyDebuff((int)Character.StatusEffects.TURNS, turns);
                finalDesc = finalDesc + " i parali¿uj¹c na " + (turns - 1) + " turê!";
            }
            AnimationId = 4;
        }
        ((Welenc)source).IncreaseAttackMultiplier();
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
            attackMultiplier = 1.5f;
            AccuracyMultiplier = 0.8f;
        }
        if (Level == 2)
        {
            criticalStuns = true;
            SkillDescription = "rozpoczyna dyskusjê o zawodnikach FPL. Zadaje obra¿enia (mentalne) losowemu przeciwnikowi, a trafienie krytyczne na krótko parali¿uje";
        }
        Level++;
    }
}
