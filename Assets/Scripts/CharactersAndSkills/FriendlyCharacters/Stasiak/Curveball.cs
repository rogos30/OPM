using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curveball : PlayableSkill
{
    float attackMultiplier = 0.4f;
    int defaultTurns = 3;
    public Curveball()
    {
        Name = "Podkrêtka";
        SkillDescription = "zagrywa przeciwnikowi podkrêcon¹ pi³eczkê. Zadaje œrednie obra¿enia i obni¿a celnoœæ wroga.";
        InFightDescription = " zagrywa podkrêcon¹ pi³eczkê, zadaj¹c ";
        Cost = 60;
        AccuracyMultiplier = 0.8f;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = false;
        AnimationId = 4;
        SkillSoundId = 16;
        MaxLevel = 2;
        levelsToUpgrades = new List<int> { 1, 5, 7 };
        tokensToUpgrades = new List<int> { 1, 2 };
        upgradeNames = new List<string> { "Odblokuj umiejêtnoœæ " + Name,"Zwiêksz obra¿enia i wyd³u¿ czas dzia³ania efektu umiejêtnoœci" };
        upgradeDescriptions = new List<string> { "Zadaje ma³e obra¿enia i obni¿a celnoœæ wroga", "+25% obra¿eñ, 2 -> 3 tury" };
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.NominativeName + " nie trafia w pi³eczkê";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName + " ";
        int damage = (int)(source.Attack * attackMultiplier * Random.Range(0.8f, 1.2f)) - target.Defense;
        int turns = defaultTurns;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            damage *= source.criticalDamageMultiplier;
            turns = defaultTurns + 2;
        }
        finalDesc = finalDesc + damage + " obra¿eñ i zmniejszaj¹c celnoœæ na " + (turns-1) + " tury!";
        target.ApplyDebuff((int)Character.StatusEffects.ACCURACY, turns);
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
            attackMultiplier = 0.6f;
            defaultTurns = 4;
        }
        Level++;
    }
}
