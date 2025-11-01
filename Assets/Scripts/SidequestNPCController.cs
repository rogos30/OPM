using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidequestNPCController : Interactable
{
    public int sideQuestId;
    public bool appearsBasedOnMainStoryProgress;
    public bool appearsBasedOnOtherSideStoryProgress;
    public int otherSideQuestRequiredId;
    [SerializeField] int[] playables;
    [SerializeField] int[] enemies;
    [SerializeField] string[] preFightLines;
    [SerializeField] AudioClip[] preFightVoiceLines;
    [SerializeField] int[] preFightSpeakersIndexes;
    [SerializeField] string[] postFightLines;
    [SerializeField] AudioClip[] postFightVoiceLines;
    [SerializeField] int[] postFightSpeakersIndexes;
    [SerializeField] bool canRunFromFight;
    [SerializeField] int backgroundId;



    public override void Interact()
    {
        DialogManager.instance.onDialogueEnd.RemoveAllListeners();
        DialogManager.instance.onDialogueEnd.AddListener(AfterFirstDialogue);
        DialogManager.instance.StartDialogue(preFightLines, preFightSpeakersIndexes, preFightVoiceLines);
    }

    public int GetProgressInSidequest()
    {
        switch(sideQuestId)
        {
            case 0:
                return StoryManager.instance.currentPingPongScamsQuest;
            case 1:
                return StoryManager.instance.currentFollowingRefereesQuest;
            case 2:
                return StoryManager.instance.currentDiversionAndSearchQuest;
            case 3:
                return StoryManager.instance.currentStrongStuffQuest;
            case 4:
                return StoryManager.instance.currentFaceTheCheaterQuest;
        }
        return -1;
    }
    

    void AfterFirstDialogue()
    {
        DialogManager.instance.onDialogueEnd.RemoveListener(AfterFirstDialogue);
        BattleManager.instance.onBattleWon.RemoveAllListeners();

        if (playables.Length > 0)
        { //battle after dialogue
            BattleManager.instance.InitiateBattle(playables, enemies, backgroundId, false, canRunFromFight);
            BattleManager.instance.onBattleWon.AddListener(AfterBattle);
        }
        else
        {
            if (interactionProgressesStory)
            { //dialogue end progresses story
                StoryManager.instance.ProgressSideQuest(sideQuestId);
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
                DialogManager.instance.onDialogueEnd.AddListener(() => StoryManager.instance.ProgressSideQuest(sideQuestId)); //after dialogue progress story
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
                StoryManager.instance.ProgressSideQuest(sideQuestId);
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
