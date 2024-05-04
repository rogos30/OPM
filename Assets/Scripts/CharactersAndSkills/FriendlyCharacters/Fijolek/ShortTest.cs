using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortTest : PlayableSkill
{    //* Kartk�wka - Fijo�ek robi niezapowiedzian� kartk�wk�, zadaje niskie obra�enia i mo�e sparali�owa�

    public ShortTest() : base()
    {
        Name = "Kartk�wka";
        SkillDescription = "robi niezapowiedzian� kartk�wk�. Zadaje wszystkim niskie obra�enia i mo�e sparali�owa�.";
        InFightDescription = " robi niezapowiedzian� kartk�wk� ";
        Cost = 100;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = true;
        TargetIsRandom = false;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.NominativeName + " u�o�y� zbyt prost� kartk�wk�";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int damage = (int)(source.Attack * 0.4f * Random.Range(0.8f, 1.2f)) - target.Defense;
        float turnsDebuff = Random.Range(0, 1f);
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            damage *= source.criticalDamageMultiplier;
            turnsDebuff *= source.criticalDamageMultiplier;
        }
        finalDesc = finalDesc + " " + damage + " obra�e�";
        if (turnsDebuff > 0.75f)
        {
            target.ApplyDebuff((int)Character.StatusEffects.TURNS, 3);
            finalDesc = finalDesc + " i zmniejszaj�c obron� na 3 tury!";
        }
        target.TakeDamage(damage);
        return finalDesc;
    }
}
