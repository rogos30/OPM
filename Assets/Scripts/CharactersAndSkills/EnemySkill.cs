using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySkill : Skill
{
    protected const float criticalChance = 0.1f;

    public EnemySkill() : base()
    {

    }

    public abstract string execute(EnemyCharacter source, Character target);
}
