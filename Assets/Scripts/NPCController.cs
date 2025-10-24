using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCController : Interactable
{
    [SerializeField] int[] playables;
    [SerializeField] int[] enemies;
    [SerializeField] string[] preFightLines;
    [SerializeField] AudioClip[] preFightVoiceLines;
    [SerializeField] int[] preFightSpeakersIndexes;
    [SerializeField] string[] postFightLines;
    [SerializeField] AudioClip[] postFightVoiceLines;
    [SerializeField] int[] postFightSpeakersIndexes;
    [SerializeField] int backgroundId;



    public override void Interact()
    {
        DialogManager.instance.onDialogueEnd.RemoveAllListeners();
        DialogManager.instance.onDialogueEnd.AddListener(AfterFirstDialogue);
        DialogManager.instance.StartDialogue(preFightLines, preFightSpeakersIndexes, preFightVoiceLines);
    }

    void AfterFirstDialogue()
    {
        DialogManager.instance.onDialogueEnd.RemoveListener(AfterFirstDialogue);
        BattleManager.instance.onBattleWon.RemoveAllListeners();

        if (playables.Length > 0)
        { //battle after dialogue
            BattleManager.instance.InitiateBattle(playables, enemies, backgroundId, false);
            BattleManager.instance.onBattleWon.AddListener(AfterBattle);
        }
        else
        {
            if (interactionProgressesStory)
            { //dialogue end progresses story
                StoryManager.instance.ProgressStory();
            }
            if (interactionSavesGame)
            { //dialogue end saves game
                GameManager.instance.SaveGame();
            }
            if (interactionBlocksSavingGame)
            {
                GameManager.instance.canSaveGame = false;
            }
            else
            {
                GameManager.instance.canSaveGame = true;
            }
        }
    }
    void AfterBattle()
    {
        BattleManager.instance.onBattleWon.RemoveListener(AfterBattle);
        if (postFightSpeakersIndexes.Length > 0)
        { //dialogue after battle
            DialogManager.instance.StartDialogue(postFightLines, postFightSpeakersIndexes, postFightVoiceLines);
            if (interactionProgressesStory)
            {
                DialogManager.instance.onDialogueEnd.AddListener(() => StoryManager.instance.ProgressStory()); //after dialogue progress story
            }
            if (interactionSavesGame)
            {
                DialogManager.instance.onDialogueEnd.AddListener(GameManager.instance.SaveGame); //after dialogue save game
            }
            if (interactionBlocksSavingGame)
            {
                DialogManager.instance.onDialogueEnd.AddListener(() => GameManager.instance.canSaveGame = false);
            }
            else
            {
                DialogManager.instance.onDialogueEnd.AddListener(() => GameManager.instance.canSaveGame = true);
            }
        }
        else
        {
            if (interactionProgressesStory)
            { //dialogue end progresses story
                StoryManager.instance.ProgressStory();
            }
            if (interactionSavesGame)
            {
                GameManager.instance.SaveGame();
            }
            if (interactionBlocksSavingGame)
            {
                GameManager.instance.canSaveGame = false;
            }
            else
            {
                GameManager.instance.canSaveGame = true;
            }
        }
    }
}
