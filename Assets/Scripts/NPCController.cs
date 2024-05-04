using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCController : Interactable
{
    [SerializeField] int[] playables;
    [SerializeField] int[] enemies;
    [SerializeField] string[] preFightLines;
    [SerializeField] int[] preFightSpeakersIndexes;
    [SerializeField] string[] postFightLines;
    [SerializeField] int[] postFightSpeakersIndexes;



    public override void Interact()
    {
        if (GameManager.instance.inGameCanvas.enabled && !DialogManager.instance.dialogueCanvas.enabled)
        {
            DialogManager.instance.onDialogueEnd.RemoveAllListeners();
            DialogManager.instance.onDialogueEnd.AddListener(AfterFirstDialogue);
            DialogManager.instance.StartDialogue(preFightLines, preFightSpeakersIndexes);
        }
    }

    void AfterFirstDialogue()
    {
        DialogManager.instance.onDialogueEnd.RemoveListener(AfterFirstDialogue);
        BattleManager.instance.onBattleWon.RemoveAllListeners();

        if (playables.Length > 0)
        { //battle after dialogue
            BattleManager.instance.InitiateBattle(playables, enemies);
            BattleManager.instance.onBattleWon.AddListener(AfterBattle);
        }
        else if (interactionProgressesStory)
        { //dialogue end progresses story
            StoryManager.instance.ProgressStory();
        }
    }
    void AfterBattle()
    {
        BattleManager.instance.onBattleWon.RemoveListener(AfterBattle);
        if (postFightSpeakersIndexes.Length > 0)
        { //dialogue after battle
            DialogManager.instance.StartDialogue(postFightLines, postFightSpeakersIndexes);
            if (interactionProgressesStory)
            {
                DialogManager.instance.onDialogueEnd.AddListener(StoryManager.instance.ProgressStory); //after dialogue progress story
            }
        }
        else if (interactionProgressesStory)
        { //dialogue end progresses story
            StoryManager.instance.ProgressStory();
        }
    }
}
