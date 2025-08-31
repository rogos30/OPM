using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DribbleThrow : EnemySkill
{
    //* Kontra � Rzuca z du�� si�� pi�k� w przeciwnika. Zadaje wysokie obra�enia jednemu przeciwnikowi.

    public DribbleThrow()
    {
        Name = "Rzut po ko�le";
        InFightDescription = " rzuca po ko�le w przeciwnik�w, odbijaj�c pi�k� mi�dzy nimi i zadaj�c ";
        Repetitions = 4;
        AccuracyMultiplier = 0.75f;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
    }

    public override string execute(EnemyCharacter source, Character target)
    {
        if (Random.Range(0, 1f) > source.Accuracy * AccuracyMultiplier)
        {
            return source.NominativeName + " nie trafia " + target.AccusativeName + " rzutem po ko�le";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName + " ";
        int damage = (int)(source.Attack * 1.25f * Random.Range(0.8f, 1.2f)) - target.Defense;
        if (Random.Range(0, 1f) < criticalChance)
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
        finalDesc = finalDesc + damage + " obra�e�!";
        return finalDesc;
    }
}
