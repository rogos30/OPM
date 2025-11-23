using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicBite : EnemySkill
{
    public ToxicBite() : base()
    {
        Name = "Toksyczne ugryzienie";
        InFightDescription = " przekazuje wœciekliznê ugryzieniem, zadaj¹c ";
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        AccuracyMultiplier = 0.8f;
    }

    public override string execute(EnemyCharacter source, Character target)
    {
        if (Random.Range(0, 1f) > source.Accuracy * AccuracyMultiplier)
        {
            return source.NominativeName + " nie trafia " + target.AccusativeName + " toksycznym ugryzieniem";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName;
        int damage = (int)(source.Attack * Random.Range(0.8f, 1.2f)) - target.Defense;

        damage = Mathf.Max(damage, 1);
        target.TakeDamage(damage);
        if (((FriendlyCharacter)target).IsGuarding && damage != 1)
        {
            damage /= 2;
        }
        finalDesc = finalDesc + damage + " obra¿eñ i truj¹c na ";
        int turns = 4;
        if (Random.Range(0, 1f) < criticalChance)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 6;
            finalDesc = finalDesc + " " + (turns - 1) + " tur!";
            damage *= source.criticalDamageMultiplier;
        }
        else
        {
            finalDesc = finalDesc + " " + (turns - 1) + " tury!";
        }


        target.ApplyDebuff((int)Character.StatusEffects.HEALTH, turns);
        return finalDesc;
    }
}
