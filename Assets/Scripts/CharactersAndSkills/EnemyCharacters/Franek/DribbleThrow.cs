using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DribbleThrow : EnemySkill
{
    //* Kontra ñ Rzuca z duøπ si≥π pi≥kπ w przeciwnika. Zadaje wysokie obraøenia jednemu przeciwnikowi.

    public DribbleThrow()
    {
        Name = "Rzut po koüle";
        InFightDescription = " rzuca po koüle w przeciwnikÛw, odbijajπc pi≥kÍ miÍdzy nimi i zadajπc ";
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
            return source.NominativeName + " nie trafia " + target.AccusativeName + " rzutem po koüle";
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
        finalDesc = finalDesc + damage + " obraøeÒ!";
        return finalDesc;
    }
}
