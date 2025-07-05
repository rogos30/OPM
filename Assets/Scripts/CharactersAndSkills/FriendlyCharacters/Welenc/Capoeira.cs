using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capoeira : PlayableSkill
{
    // Start is called before the first frame update
    public Capoeira()
    {
        Name = "Capoeira";
        SkillDescription = "ta�czy capoeir�, �aduj�c mno�nik ataku do pe�na (" + Welenc.MaxAttackMultiplier + ")";
        InFightDescription = " ta�czy capoeir� i zwi�ksza sw�j mno�nik ataku ";
        Cost = 0.5f;
        TargetIsFriendly = false;
        TargetIsSelf = true;
        MultipleTargets = false;
        TargetIsRandom = false;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            int damage = Random.Range(source.MaxHealth / 7, source.MaxHealth / 4);
            source.TakeDamage(damage);
            return source.NominativeName + " wywala si� na twarz, otrzymuj�c " + damage + " obra�e�";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc + " ponad limit!";
            ((Welenc)source).MaxOutAttackMultiplier(true);
        }
        else
        {
            finalDesc = finalDesc + " do limitu!";
            ((Welenc)source).MaxOutAttackMultiplier(false);
        }
        return finalDesc;
    }
}
