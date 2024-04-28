using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetrayableChangeOfRules : PlayableSkill
{

    public BetrayableChangeOfRules()
    {
        Name = "Zmiana zasad";
        SkillDescription = "uznaje, ¿e przegrywa, wiêc usuwa turê przeciwnika.";
        InFightDescription = " parali¿uje ";
        Cost = 0.4f;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = false;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.NominativeName + " jednak gra wed³ug regu³";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.AccusativeName;
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
        int turns = 2;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 3;
        }
        finalDesc = finalDesc + " na " + turns + " tur!";
        target.ApplyDebuff(4, turns);
        return finalDesc;
    }
}
