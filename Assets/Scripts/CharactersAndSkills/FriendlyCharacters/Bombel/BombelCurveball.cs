using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombelCurveball : PlayableSkill
{
    public BombelCurveball()
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
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        int upgrade = Random.Range(0, 5);
        if (upgrade == 0)
        {
            skillPerformance++;
            Mathf.Min(skillPerformance, 2);
        }
        if (skillPerformance == 0)
        {
            return source.NominativeName + " nie trafia w pi³eczkê";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName + " ";
        int damage = (int)(source.Attack * 0.4f * Random.Range(0.8f, 1.2f)) - target.Defense;
        int turns = 3;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            damage *= source.criticalDamageMultiplier;
            turns = 5;
        }
        finalDesc = finalDesc + damage + " obra¿eñ i zmniejszaj¹c celnoœæ na " + (turns - 1) + " tury!";
        target.ApplyDebuff((int)Character.StatusEffects.ACCURACY, turns);
        target.TakeDamage(damage);
        return finalDesc;
    }
}
