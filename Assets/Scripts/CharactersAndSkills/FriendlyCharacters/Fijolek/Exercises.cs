using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exercises : PlayableSkill
{    
    //* Zadanka - Fijo�ek zadaje mn�stwo zadanek, zadaje niskie obra�enia i mo�e zmniejszy� celno��

    public Exercises() : base()
    {
        Name = "Zadanka";
        SkillDescription = "zadaje mn�stwo zadanek, zadaje wszystkim niskie obra�enia i mo�e zmniejszy� celno��.";
        InFightDescription = " zadaje ci�kie zadanka ";
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
            return source.NominativeName + " jednak nie zada� zadanek (niebazowane na faktach)";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int damage = (int)(source.Attack * 0.4f * Random.Range(0.8f, 1.2f)) - target.Defense;
        float accDebuff = Random.Range(0, 1f);
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            damage *= source.criticalDamageMultiplier;
            accDebuff *= source.criticalDamageMultiplier;
        }
        finalDesc = finalDesc + " " + damage + " obra�e�";
        if (accDebuff > 0.75f)
        {
            target.ApplyDebuff((int)Character.StatusEffects.ACCURACY, 3);
            finalDesc = finalDesc + " i zmniejszaj�c obron� na 3 tury!";
        }
        target.TakeDamage(damage);
        return finalDesc;
    }
}
