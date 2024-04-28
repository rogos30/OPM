using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection : EnemySkill
{
    //skill = new Skill("Po³¹czenie", "³¹czy siê z pozosta³ymi urz¹dzeniami. Zwiêksza obra¿enia dru¿ynie.", "³¹czy siê z pozosta³ymi urz¹dzeniami. Zwiêksza obra¿enia", 110, 1, 0, 0.5f, 0, false, false, false, true, statusEffects);

    public Connection() : base()
    {
        Name = "Po³¹czenie";
        InFightDescription = " ³¹czy siê z pozosta³ymi urz¹dzeniami. Zwiêksza obra¿enia ";
        TargetIsFriendly = true;
        TargetIsSelf = false;
        MultipleTargets = true;
        AccuracyMultiplier = 0.5f;
    }

    public override string execute(EnemyCharacter source, Character target)
    {
        if (Random.Range(0, 1f) > source.Accuracy * AccuracyMultiplier)
        {
            return source.DativeName + " nie udaje siê pod³¹czyæ do " + target.AccusativeName;
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
