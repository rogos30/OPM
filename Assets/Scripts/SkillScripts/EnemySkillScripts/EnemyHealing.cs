using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealing : EnemySkill
{
    public EnemyHealing() : base()
    {
        Name = "Leczenie";
        InFightDescription = " leczy ";
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
    }

    public override string execute(EnemyCharacter source, Character target)
    {
        if (Random.Range(0, 1f) < source.Accuracy * AccuracyMultiplier)
        {
            return source.NominativeName + "nie trafia leczeniem w " + target.AccusativeName;
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName;
        int healing = 100;
        if (Random.Range(0, 1f) < criticalChance)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            healing *= source.criticalDamageMultiplier;
        }
        target.Heal(healing);
        finalDesc = finalDesc + target.DativeName + " " + healing + " zdrowia!";
        return finalDesc;
    }
}
