using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdMotivation : EnemySkill
{
    public CrowdMotivation() : base()
    {
        Name = "Motywacja od t�umu";
        InFightDescription = " pozyskuje motywacj� od t�umu ";
        TargetIsFriendly = false;
        TargetIsSelf = true;
        MultipleTargets = false;
        SkillSoundId = 40;
    }

    public override string execute(EnemyCharacter source, Character target)
    {
        if (Random.Range(0, 1f) > source.Accuracy * AccuracyMultiplier)
        {
            return source.NominativeName + " nie ws�uchuje si� w t�um";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName + ", odnawiaj�c sobie ";
        int healing = (int)(source.MaxHealth / 10 * Random.Range(0.8f, 1.2f));
        int turns = 3;
        if (Random.Range(0, 1f) < criticalChance)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 4;
            healing *= source.criticalDamageMultiplier;
        }
        target.ApplyBuff((int)Character.StatusEffects.ATTACK, turns);
        target.ApplyBuff((int)Character.StatusEffects.DEFENSE, turns);
        healing = Mathf.Max(healing, 1);
        target.Heal(healing);
        finalDesc = finalDesc + healing + " zdrowia i wzmacniaj�c sw�j atak i obron� na " + (turns - 1) + " tury!";
        return finalDesc;
    }
}
