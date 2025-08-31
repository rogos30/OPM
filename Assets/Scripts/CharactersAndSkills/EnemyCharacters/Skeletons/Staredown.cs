using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staredown : EnemySkill
{
    public Staredown() : base()
    {
        Name = "Przenikliwe spojrzenie";
        InFightDescription = " przera¿aj¹co patrzy na ";
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        AccuracyMultiplier = 2f;
        AnimationId = 4;
    }

    public override string execute(EnemyCharacter source, Character target)
    {
        if (Random.Range(0, 1f) > source.Accuracy * AccuracyMultiplier)
        {
            return source.DativeName + " wzrok zje¿d¿a z " + target.AccusativeName;
        }
        string finalDesc = source.NominativeName + InFightDescription + target.AccusativeName + ", anuluj¹c wszystkie wzmocnienia";

        for (int i = 0; i < target.StatusTimers.Length; i++)
        {
            if (target.StatusTimers[i] > 0)
            {
                target.ApplyDebuff(i, 1);
            }
        }

        return finalDesc;
    }
}
