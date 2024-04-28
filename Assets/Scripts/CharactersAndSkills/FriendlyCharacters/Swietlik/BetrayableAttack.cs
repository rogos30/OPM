using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetrayableAttack : PlayableSkill
{
    public BetrayableAttack()
    {
        Name = "Atak";
        SkillDescription = "wyprowadza zwyk³y cios. Mo¿e zdradziæ!";
        InFightDescription = " wyprowadza zwyk³y cios i zadaje ";
        Cost = 0;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = false;
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
                finalDesc = "NAST¥PI£A ZDRADA! ";
            }
            else
            {
                betrayed = false;
            }
        }
        finalDesc += source.NominativeName + InFightDescription + target.DativeName;
        int damage = (int)(source.Attack * Random.Range(0.8f, 1.2f) * (100 + ((Swietlik)source).Betrayal) / 100) - target.Defense;
        if (betrayed && ((FriendlyCharacter)target).IsGuarding)
        {
            damage = (int)(damage * FriendlyCharacter.guardDamageMultiplier);
        }
        if (betrayed )
        {
            damage /= 2;
        }
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            damage *= source.criticalDamageMultiplier;
        }
        damage = Mathf.Max(damage, 1);
        finalDesc = finalDesc + " " + damage + " obra¿eñ!";
        target.TakeDamage(damage);
        ((Swietlik)source).ResetBetrayal();
        return finalDesc;
    }
}
