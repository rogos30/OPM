using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Rogos : FriendlyCharacter
{
    //specialty - guard lasts 2 turns and gives twice as much Skill
    private int GuardTimer { get; set; }
    public Rogos() : base()
    {
        NominativeName = "Rogos";
        DativeName = "Rogosowi";
        AccusativeName = "Rogosa";
        Health = MaxHealth = DefaultMaxHealth = 400;
        Skill = MaxSkill = 100;
        Attack = DefaultAttack = 80;
        Defense = DefaultDefense = 30;
        Accuracy = DefaultAccuracy = 0.95f;
        Turns = DefaultTurns = 1;
        Speed = 600;
        SpriteIndex = 0;
        GuardTimer = 0;
        AbilityDescription = "Garda Rogosa trwa 2 tury i odnawia 40% SP";
        CharacterDescription = "Opis Rogosa wip";
        Attack attack = new Attack();
        ControllerThrow controllerThrow = new ControllerThrow();
        Cheats cheats = new Cheats();
        MoraleBoost moraleBoost = new MoraleBoost();
        ControllerBarrage controllerBarrage = new ControllerBarrage();
        ZahirTrip zahirTrip = new ZahirTrip();
        skillSet.Add(attack);
        skillSet.Add(controllerThrow);
        skillSet.Add(cheats);
        skillSet.Add(moraleBoost);
        skillSet.Add(controllerBarrage);
        skillSet.Add(zahirTrip);
    }

    public override void StartGuard()
    {
        GuardTimer = 2;
        IsGuarding = true;
        RestoreSkill(0.4f);
        HealingMultiplier = 1.5f;
    }

    public override void HandleGuard()
    {
        if (GuardTimer > 0)
        {
            GuardTimer--;
        }
        if (GuardTimer == 0)
        {
            IsGuarding = false;
            HealingMultiplier = 1;
        }
    }
    protected override void AdditionalChangesOnReset()
    {
        Skill = MaxSkill;
        IsGuarding = false;
        ((ZahirTrip)skillSet[5]).SetToMission();
        ((ControllerBarrage)skillSet[4]).SetToUsable();
    }
}
