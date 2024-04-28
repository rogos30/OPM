using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection : EnemySkill
{
    //skill = new Skill("Po��czenie", "��czy si� z pozosta�ymi urz�dzeniami. Zwi�ksza obra�enia dru�ynie.", "��czy si� z pozosta�ymi urz�dzeniami. Zwi�ksza obra�enia", 110, 1, 0, 0.5f, 0, false, false, false, true, statusEffects);

    public Connection() : base()
    {
        Name = "Po��czenie";
        InFightDescription = " ��czy si� z pozosta�ymi urz�dzeniami. Zwi�ksza obra�enia ";
        TargetIsFriendly = true;
        TargetIsSelf = false;
        MultipleTargets = true;
        AccuracyMultiplier = 0.5f;
    }

    public override string execute(EnemyCharacter source, Character target)
    {
        if (Random.Range(0, 1f) > source.Accuracy * AccuracyMultiplier)
        {
            return source.DativeName + " nie udaje si� pod��czy� do " + target.AccusativeName;
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName;
        int turns = 2;
        if (Random.Range(0, 1f) < criticalChance)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 4;
        }
        finalDesc = finalDesc + " na " + turns + " tur!";
        target.ApplyBuff(0, turns);
        return finalDesc;
    }
}
