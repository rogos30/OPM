using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetrayableTriplePunch : PlayableSkill
{
    //skill = new Skill("Potr�jny atak", "wyprowadza 3 zwyk�e ciosy", "wyprowadza 3 zwyk�e ciosy", 35, 3, 1, 1, 0, false, false, true, false, statusEffects);

    public BetrayableTriplePunch()
    {
        Name = "Potr�jny atak";
        SkillDescription = "wyprowadza 3 zwyk�e ciosy. Mo�e zdradzi�!";
        InFightDescription = " wyprowadza 3 zwyk�e ciosy ";
        Cost = 0.2f;
        Repetitions = 3;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = true;
    }
    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.NominativeName + " nie trafia atakiem w " + target.AccusativeName;
        }
        string finalDesc = "";
        bool betrayed = false;
        if (((Swietlik)source).Betrayal > Random.Range(0, 100))
        {   //choose a new target from your companions
            int knockedOuts = 0, newTargetId;
            betrayed = true;
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
                finalDesc = "NAST�PI�A ZDRADA! ";
            }
        }
        finalDesc += source.NominativeName + InFightDescription + target.DativeName;
        int damage = (int)(source.Attack * Random.Range(0.8f, 1.2f) * (100 + ((Swietlik)source).Betrayal) / 100) - target.Defense;
        if (betrayed && ((FriendlyCharacter)target).IsGuarding)
        {
            damage = (int)(damage * FriendlyCharacter.guardDamageMultiplier);
        }
        if (betrayed)
        {
            damage /= 2;
        }
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            damage *= source.criticalDamageMultiplier;
        }
        damage = Mathf.Max(damage, 1);
        target.TakeDamage(damage);
        if (((FriendlyCharacter)target).IsGuarding && damage != 1)
        {
            damage /= 2;
        }
        finalDesc = finalDesc + " " + damage + " obra�e�!";
        return finalDesc;
    }
}
