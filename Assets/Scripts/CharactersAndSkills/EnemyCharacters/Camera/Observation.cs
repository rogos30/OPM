using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observation : EnemySkill
{
    //skill = new Skill("Inwigilacja", "przegl�da wrog�w na wylot, os�abiaj�c ich. Zmniejsza obra�enia przeciwnikom.", "przegl�da wrog�w na wylot, os�abiaj�c ich. Zmniejsza obra�enia", 110, 1, 0, 0.5f, 0, false, false, false, true, statusEffects);

    public Observation() : base()
    {
        Name = "Inwigilacja";
        InFightDescription = " przegl�da ";
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
            return source.DativeName + " nie udaje si� przejrze� " + target.AccusativeName;
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
