using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipliableAttack : PlayableSkill
{
    public MultipliableAttack()
    {
        Name = "Atak";
        SkillDescription = "wyprowadza zwyk�y cios. Moc zale�na od mno�nika";
        InFightDescription = " wyprowadza zwyk�y cios i zadaje ";
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
        
        finalDesc += source.NominativeName + InFightDescription + target.DativeName;
        int damage = (int)(source.Attack * Random.Range(0.8f, 1.2f) * ((Welenc)source).AttackMultiplier) - target.Defense;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            damage *= source.criticalDamageMultiplier;
        }
        damage = Mathf.Max(damage, 1);
        finalDesc = finalDesc + " " + damage + " obra�e�!";
        target.TakeDamage(damage);
        ((Welenc)source).ResetAttackMultiplier();
        return finalDesc;
    }
}
