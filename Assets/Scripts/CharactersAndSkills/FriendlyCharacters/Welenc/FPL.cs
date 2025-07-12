using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPL : PlayableSkill
{
    public FPL()
    {
        Name = "FPL";
        SkillDescription = "rozpoczyna dyskusjê o zawodnikach FPL. Zadaje obra¿enia (mentalne) losowemu przeciwnikowi, a trafienie krytyczne na krótko parali¿uje";
        InFightDescription = " zagaduje ";
        Cost = 0.33f;
        AccuracyMultiplier = 0.67f;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = true;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        AnimationId = 1;
        if (skillPerformance == 0)
        {
            return target.NominativeName + " ka¿e " + source.DativeName + " siê goniæ";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.AccusativeName;
        int damage = (int)(source.Attack * 1.2f * Random.Range(0.8f, 1.2f)) - target.Defense;
        damage = Mathf.Max(damage, 1);
        target.TakeDamage(damage);
        finalDesc = finalDesc + ", zadaj¹c " + damage + " obra¿eñ";
        if (skillPerformance == 2)
        {
            int turns = 2;
            target.ApplyDebuff((int)Character.StatusEffects.TURNS, turns);
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc + " i parali¿uj¹c na " + (turns - 1) + " turê!";
            AnimationId = 4;
        }
        ((Welenc)source).IncreaseAttackMultiplier();
        return finalDesc;
    }
}
