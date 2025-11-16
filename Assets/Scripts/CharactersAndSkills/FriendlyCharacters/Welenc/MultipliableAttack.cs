using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipliableAttack : PlayableSkill
{
    public MultipliableAttack() : base()
    {
        Name = "Atak";
        SkillDescription = "wyprowadza zwyk³y cios. Moc zale¿na od mno¿nika";
        InFightDescription = " wyprowadza zwyk³y cios i zadaje ";
        Cost = 0;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = false;
        SkillSoundId = 26;
        IsUnlocked = true;
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
        finalDesc = finalDesc + " " + damage + " obra¿eñ!";
        target.TakeDamage(damage);
        ((Welenc)source).ResetAttackMultiplier();
        return finalDesc;
    }

    public override void upgrade()
    {
        return;
    }
}
