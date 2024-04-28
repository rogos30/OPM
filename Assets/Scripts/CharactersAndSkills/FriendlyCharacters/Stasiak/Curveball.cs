using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curveball : PlayableSkill
{
    //skill = new Skill("Podkrêtka", "zagrywa przeciwnikowi podkrêcon¹ pi³eczkê. Zadaje œrednie obra¿enia i obni¿a celnoœæ wroga.", "zagrywa podkrêcon¹ pi³eczkê", 60, 1, 0.4f, 0.8f, 0, false, false, false, false, statusEffects);

    public Curveball()
    {
        Name = "Podkrêtka";
        SkillDescription = "zagrywa przeciwnikowi podkrêcon¹ pi³eczkê. Zadaje œrednie obra¿enia i obni¿a celnoœæ wroga.";
        InFightDescription = " zagrywa podkrêcon¹ pi³eczkê i ";
        Cost = 60;
        AccuracyMultiplier = 0.8f;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = false;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.NominativeName + " nie trafia w pi³eczkê";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int damage = (int)(source.Attack * 0.4f * Random.Range(0.8f, 1.2f)) - target.Defense;
        int turns = 3;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            damage *= source.criticalDamageMultiplier;
            turns = 5;
        }
        finalDesc = finalDesc + " " + turns + " tur!";
        target.StatusTimers[2] = -turns;
        target.TakeDamage(damage);
        return finalDesc;
    }
}
