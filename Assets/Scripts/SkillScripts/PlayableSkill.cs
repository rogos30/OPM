using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayableSkill : Skill
{
    public string SkillDescription { get; set; }
    public float Cost { get; set; }
    public bool TargetIsRandom { get; set; }

    public PlayableSkill() : base()
    {

    }

    public abstract string execute(FriendlyCharacter source, Character target, int skillPerformance);
}
