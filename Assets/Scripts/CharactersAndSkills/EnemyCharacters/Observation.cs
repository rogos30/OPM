using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observation : EnemySkill
{
    //skill = new Skill("Inwigilacja", "przegl¹da wrogów na wylot, os³abiaj¹c ich. Zmniejsza obra¿enia przeciwnikom.", "przegl¹da wrogów na wylot, os³abiaj¹c ich. Zmniejsza obra¿enia", 110, 1, 0, 0.5f, 0, false, false, false, true, statusEffects);

    public Observation() : base()
    {
        Name = "Inwigilacja";
        InFightDescription = " przegl¹da ";
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = true;
        AccuracyMultiplier = 0.5f;
        SkillSoundId = 34;
    }

    public override string execute(EnemyCharacter source, Character target)
    {
        if (Random.Range(0, 1f) > source.Accuracy * AccuracyMultiplier)
        {
            return source.DativeName + " nie udaje siê przejrzeæ " + target.AccusativeName;
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName;
        int turns = 2;
        if (Random.Range(0, 1f) < criticalChance)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 4;
        }
        finalDesc = finalDesc + " i zmniejsza atak na " + turns + " tur!";
        target.ApplyDebuff(0, turns);
        return finalDesc;
    }
}
