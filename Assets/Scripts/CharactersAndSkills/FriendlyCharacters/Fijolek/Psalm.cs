using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Psalm : PlayableSkill
{
    //* Psalm - Fijo�ek nuci psalm, zadaje niskie obra�enia i mo�e zmniejszy� obron� 

    public Psalm() : base()
    {
        Name = "Psalm";
        SkillDescription = "�piewa psalm. Zadaje wszystkim przeciwnikom niskie obra�enia i mo�e zmniejszy� obron�.";
        InFightDescription = " chwali Pana, zadaj�c ";
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
            return source.NominativeName + " oddala si� od Boga";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int damage = (int)(source.Attack * 0.4f * Random.Range(0.8f, 1.2f)) - target.Defense;
        float defenseDebuff = Random.Range(0, 1f);
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            damage *= source.criticalDamageMultiplier;
            defenseDebuff *= source.criticalDamageMultiplier;
        }
        finalDesc = finalDesc + " " + damage + " obra�e�";
        if (defenseDebuff > 0.75f)
        {
            target.ApplyDebuff((int)Character.StatusEffects.DEFENSE, 3);
            finalDesc = finalDesc + " i zmniejszaj�c obron� na 3 tury!";
        }
        target.TakeDamage(damage);
        return finalDesc;
    }
}
