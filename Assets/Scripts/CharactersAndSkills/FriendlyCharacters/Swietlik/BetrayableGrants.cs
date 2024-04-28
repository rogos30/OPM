using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetrayableGrants : PlayableSkill
{    
    //skill = new Skill("Grant's", "proponuje przeciwnikowi Grant'sa. Mo¿e otruæ", "otruwa Grant'sem w¹trobê", 40, 1, 0, 0.5f, 0, false, false, false, false, statusEffects);

    public BetrayableGrants() : base()
    {
        Name = "Grant's";
        SkillDescription = "proponuje przeciwnikowi Grant'sa. Mo¿e otruæ";
        InFightDescription = " otruwa Grant'sem w¹trobê ";
        Cost = 90;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = false;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return target.NominativeName + "odmawia Grant'sa!";
        }
        string finalDesc = "";
        if (((Swietlik)source).Betrayal > Random.Range(0, 100))
        {   //choose a new target from your companions
            int knockedOuts = 0, newTargetId;
            foreach (var companion in BattleManager.instance.playableCharacters)
            {
                if (companion.KnockedOut)
                {
                    knockedOuts++;
                }
            }
            if (knockedOuts + 1 < BattleManager.instance.playableCharacters.Count)
            { //there is someone else alive besides Swietlik
                do
                {
                    newTargetId = Random.Range(0, BattleManager.instance.playableCharacters.Count);
                } while (BattleManager.instance.playableCharacters[newTargetId] == source || BattleManager.instance.playableCharacters[newTargetId].KnockedOut);
                target = BattleManager.instance.playableCharacters[newTargetId];
                finalDesc = "NAST¥PI£A ZDRADA! ";
            }
        }
        finalDesc = source.NominativeName + InFightDescription + target.DativeName;
        int turns = 3;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 5;
        }
        finalDesc = finalDesc + " na " + turns + " tur!";
        target.ApplyDebuff(3, turns);
        ((Swietlik)source).ResetBetrayal();
        return finalDesc;
    }
}
