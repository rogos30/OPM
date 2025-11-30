using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SidequestTriggerController : Interactable
{
    public int sideQuestId;
    public bool appearsBasedOnMainStoryProgress;
    public bool appearsBasedOnOtherSideStoryProgress;
    public int otherSideQuestRequiredId;
    [SerializeField] string[] lines;
    [SerializeField] AudioClip[] voiceLines;
    [SerializeField] int[] speakersIndexes;
    public override void Interact()
    {
        if (lines.Length > 0)
        {
            DialogueManager.instance.onDialogueEnd.RemoveAllListeners();
            DialogueManager.instance.onDialogueEnd.AddListener(AfterDialogue);
            DialogueManager.instance.StartDialogue(lines, speakersIndexes, voiceLines);
        }
    }

    public int GetProgressInSidequest()
    {
        switch (sideQuestId)
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

    void AfterDialogue()
    {
        DialogueManager.instance.onDialogueEnd.RemoveListener(AfterDialogue);

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
