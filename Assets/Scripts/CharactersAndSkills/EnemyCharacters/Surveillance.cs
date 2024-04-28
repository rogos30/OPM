using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surveillance : EnemySkill
{
    //skill = new Skill("Podgl¹d", "przygl¹da siê rywalom, dekoncentruj¹c ich. Zmniejsza celnoœæ przeciwnikom.", "przygl¹da siê rywalom, dekoncentruj¹c ich. Zmniejsza celnoœæ", 110, 1, 0, 0.5f, 0, false, false, false, true, statusEffects);

    public Surveillance() : base()
    {
        Name = "Podgl¹d";
        InFightDescription = " przygl¹da siê ";
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = true;
        AccuracyMultiplier = 0.5f;
    }

    public override string execute(EnemyCharacter source, Character target)
    {
        if (Random.Range(0, 1f) > source.Accuracy * AccuracyMultiplier)
        {
            return source.DativeName + " nie udaje siê podejrzeæ " + target.AccusativeName;
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName;
        int turns = 2;
        if (Random.Range(0, 1f) < criticalChance)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 4;
        }
        finalDesc = finalDesc + " i zmniejsza celnoœæ na " + turns + " tur!";
        target.ApplyDebuff(2, turns);
        return finalDesc;
    }
}
