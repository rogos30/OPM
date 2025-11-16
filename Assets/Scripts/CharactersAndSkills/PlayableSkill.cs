using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayableSkill : Skill
{
    public string SkillDescription { get; set; }
    public float Cost { get; set; }
    public bool TargetIsRandom { get; set; }
    public int Level { get; set; }
    public int MaxLevel { get; set; }
    public List<int> levelsToUpgrades;
    public List<int> tokensToUpgrades;
    public List<string> upgradeNames;
    public List<string> upgradeDescriptions;
    public bool IsUnlocked { get; set; }


    public PlayableSkill() : base()
    {
        IsUnlocked = false;
        Level = 0;
    }

    public abstract string execute(FriendlyCharacter source, Character target, int skillPerformance);
    public abstract void upgrade();
}
