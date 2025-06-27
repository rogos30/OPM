using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicGas : EnemySkill
{
    //skill = new Skill("Pierd", "pierdzi na przeciwników", "wypuszcza chmurê gazu", 40, 1, 1, 0.8f, 0, false, false, false, true, statusEffects);

    public ToxicGas() : base()
    {
        Name = "Toksyczny gaz";
        InFightDescription = " wypuszcza chmurê gazu i zadaje ";
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = true;
        AccuracyMultiplier = 0.8f;
    }

    public override string execute(EnemyCharacter source, Character target)
    {
        if (Random.Range(0, 1f) > source.Accuracy * AccuracyMultiplier)
        {
            return target.NominativeName + " wykazuje odpornoœæ na gaz";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName;
        int damage = (int)(source.Attack * Random.Range(0.8f, 1.2f) - target.Defense);
        if (Random.Range(0, 1f) < criticalChance)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            damage *= 2;
        }

        damage = Mathf.Max(damage, 1);
        target.TakeDamage(damage);
        if (((FriendlyCharacter)target).IsGuarding && damage != 1)
        {
            damage /= 2;
        }
        finalDesc = finalDesc + " " + damage + " obra¿eñ!";
        return finalDesc;
    }
}
