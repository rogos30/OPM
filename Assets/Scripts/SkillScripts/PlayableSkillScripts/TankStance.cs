using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankStance : PlayableSkill
{
    //skill = new Skill("Postawa tanka", "zwiêksza swoj¹ obronê i leczy do 40% hp.", "wchodzi w postawê tanka", 65, 1, 0, 1, 0.4f, false, true, false, false, statusEffects);

    public TankStance()
    {
        Name = "Postawa tanka";
        SkillDescription = "zwiêksza swoj¹ obronê i leczy do 40% hp.";
        InFightDescription = "wchodzi w postawê tanka ";
        Cost = 140f;
        TargetIsFriendly = false;
        TargetIsSelf = true;
        MultipleTargets = false;
        TargetIsRandom = false;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.NominativeName + " nie zostaje tankiem.";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int turns = 3;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 5;
        }
        finalDesc = finalDesc + " " + turns + " tur!";
        target.StatusTimers[1] = turns;
        ((Swietlik)source).ReduceBetrayal();
        return finalDesc;
    }
}
