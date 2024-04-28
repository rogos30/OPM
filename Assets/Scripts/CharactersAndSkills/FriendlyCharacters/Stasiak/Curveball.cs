using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curveball : PlayableSkill
{
    //skill = new Skill("Podkr�tka", "zagrywa przeciwnikowi podkr�con� pi�eczk�. Zadaje �rednie obra�enia i obni�a celno�� wroga.", "zagrywa podkr�con� pi�eczk�", 60, 1, 0.4f, 0.8f, 0, false, false, false, false, statusEffects);

    public Curveball()
    {
        Name = "Podkr�tka";
        SkillDescription = "zagrywa przeciwnikowi podkr�con� pi�eczk�. Zadaje �rednie obra�enia i obni�a celno�� wroga.";
        InFightDescription = " zagrywa podkr�con� pi�eczk� i ";
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
            return source.NominativeName + " nie trafia w pi�eczk�";
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
