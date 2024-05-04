using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortTest : PlayableSkill
{    //* Kartkówka - Fijo³ek robi niezapowiedzian¹ kartkówkê, zadaje niskie obra¿enia i mo¿e sparali¿owaæ

    public ShortTest() : base()
    {
        Name = "Kartkówka";
        SkillDescription = "robi niezapowiedzian¹ kartkówkê. Zadaje wszystkim niskie obra¿enia i mo¿e sparali¿owaæ.";
        InFightDescription = " robi niezapowiedzian¹ kartkówkê ";
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
            return source.NominativeName + " u³o¿y³ zbyt prost¹ kartkówkê";
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
        finalDesc = finalDesc + " " + damage + " obra¿eñ";
        if (turnsDebuff > 0.75f)
        {
            target.ApplyDebuff((int)Character.StatusEffects.TURNS, 3);
            finalDesc = finalDesc + " i zmniejszaj¹c obronê na 3 tury!";
        }
        target.TakeDamage(damage);
        return finalDesc;
    }
}
