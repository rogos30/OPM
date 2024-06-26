using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortTest : PlayableSkill
{    //* Kartkówka - Fijołek robi niezapowiedzianą kartkówkę, zadaje niskie obrażenia i może sparaliżować

    public ShortTest() : base()
    {
        Name = "Kartkówka";
        SkillDescription = "robi niezapowiedzianą kartkówkę. Zadaje wszystkim niskie obrażenia i może sparaliżować.";
        InFightDescription = " robi niezapowiedzianą kartkówkę ";
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
            return source.NominativeName + " ułożył zbyt prostą kartkówkę";
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
        finalDesc = finalDesc + " " + damage + " obrażeń";
        if (turnsDebuff > 0.75f)
        {
            target.ApplyDebuff((int)Character.StatusEffects.TURNS, 3);
            finalDesc = finalDesc + " i zmniejszając obronę na 3 tury!";
        }
        target.TakeDamage(damage);
        return finalDesc;
    }
}
