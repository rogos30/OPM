using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Welenc : FriendlyCharacter
{
    public float AttackMultiplier { get; set; }
    public const float MaxAttackMultiplier = 4f;
    public Welenc() : base()
    {
        NominativeName = "Welenc";
        DativeName = "Welencowi";
        AccusativeName = "Welenca";
        Health = MaxHealth = DefaultMaxHealth = 450;
        Skill = MaxSkill = 110;
        Attack = DefaultAttack = 80;
        Defense = DefaultDefense = 30;
        Accuracy = DefaultAccuracy = 1;
        Turns = DefaultTurns = 1;
        Speed = 500;
        SpriteIndex = 5;
        AbilityDescription = "Welenca podstawowy atak jest modyfikowany o mno¿nik. Mno¿nik zwiêksza siê za ka¿dego u¿ytego skilla. Obecnie: x" + AttackMultiplier;
        CharacterDescription = "Opis Welenca wip";
        MultipliableAttack attack = new MultipliableAttack();
        skillSet.Add(attack);
        LaughingStock laughingStock = new LaughingStock();
        skillSet.Add(laughingStock);
        Aphrodite aphrodite = new Aphrodite();
        skillSet.Add(aphrodite);
        FPL fpl = new FPL();
        skillSet.Add(fpl);
        DiscoPoloStar discoPoloStar = new DiscoPoloStar();
        skillSet.Add(discoPoloStar);
        Capoeira capoeira = new Capoeira();
        skillSet.Add(capoeira);
    }

    public void ResetAttackMultiplier()
    {
        AttackMultiplier = 0.5f;
        AbilityDescription = "Welenca podstawowy atak jest modyfikowany o mno¿nik. Mno¿nik zwiêksza siê za ka¿dego u¿ytego skilla. Obecnie: x" + AttackMultiplier;
    }

    public void IncreaseAttackMultiplier()
    {
        AttackMultiplier = Mathf.Min(AttackMultiplier + 0.5f, MaxAttackMultiplier);
        AbilityDescription = "Welenca podstawowy atak jest modyfikowany o mno¿nik. Mno¿nik zwiêksza siê za ka¿dego u¿ytego skilla. Obecnie: x" + AttackMultiplier;
    }

    public void MaxOutAttackMultiplier(bool overcharge)
    {
        AttackMultiplier = MaxAttackMultiplier;
        if (overcharge)
        {
            AttackMultiplier += 1;
        }
        AbilityDescription = "Welenca podstawowy atak jest modyfikowany o mno¿nik. Mno¿nik zwiêksza siê za ka¿dego u¿ytego skilla. Obecnie: x" + AttackMultiplier;
    }

    protected override void AdditionalChangesOnReset()
    {
        base.AdditionalChangesOnReset();
        ResetAttackMultiplier();
    }
}
