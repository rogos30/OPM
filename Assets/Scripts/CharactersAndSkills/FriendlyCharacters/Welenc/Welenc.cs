using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Welenc : FriendlyCharacter
{
    public float AttackMultiplier { get; set; }
    public const float MaxAttackMultiplier = 4f;
    public const float MultiplierIncreaseFromSkill = 1.25f;
    public const float MultiplierIncreaseFromGuard = 0.75f;
    public Welenc() : base()
    {
        NominativeName = "Welenc";
        DativeName = "Welencowi";
        AccusativeName = "Welenca";

        Health = MaxHealth = DefaultMaxHealth = BaseMaxHealth = 450;
        Skill = MaxSkill = 110;
        Attack = DefaultAttack = BaseAttack = 80;
        Defense = DefaultDefense = BaseDefense = 30;
        Accuracy = DefaultAccuracy = BaseAccuracy = 1;

        Turns = DefaultTurns = 1;
        Speed = 500;
        SpriteIndex = 1;
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
    public override void StartGuard()
    {
        base.StartGuard();
        IncreaseAttackMultiplier(MultiplierIncreaseFromGuard);
    }
    public void ResetAttackMultiplier()
    {
        AttackMultiplier = 0.25f;
        AbilityDescription = "Welenca podstawowy atak jest modyfikowany o mno¿nik. Mno¿nik zwiêksza siê za ka¿dego u¿ytego skilla. Obecnie: x" + AttackMultiplier;
    }

    public void IncreaseAttackMultiplier(float multiplierGain = MultiplierIncreaseFromSkill)
    {
        if (AttackMultiplier < MaxAttackMultiplier)
        {
            AttackMultiplier = Mathf.Min(AttackMultiplier + multiplierGain, MaxAttackMultiplier);
        }
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
