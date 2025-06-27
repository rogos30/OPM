using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    public int DifficultyHealthChange { get; set; }
    public int DifficultyAttackChange { get; set; }
    public int MoneyDropped { get; set; }
    public int XPDropped { get; set; }
    public List<EnemySkill> skillSet = new List<EnemySkill>();

    public void UpdateDifficulty(int difficulty)
    {
        MaxHealth = DefaultMaxHealth + difficulty * DifficultyHealthChange;
        Attack = DefaultAttack + difficulty * DifficultyAttackChange;
    }

    public override void HandleTimers()
    {
        for (int i = 0; i < StatusTimers.Length; i++)
        {
            if (StatusTimers[i] > 0)
            {
                StatusTimers[i]--;
            }
            else if (StatusTimers[i] < 0)
            {
                StatusTimers[i]++;
            }
            if (StatusTimers[i] == 0)
            {
                CancelStatusEffect(i);
            }
        }
        if (NegativeEffectsImmunity > 0)
        {
            NegativeEffectsImmunity--;
        }
    }
}
