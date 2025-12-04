using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TriggerController : Interactable
{
    [SerializeField] string[] lines;
    [SerializeField] AudioClip[] voiceLines;
    [SerializeField] int[] speakersIndexes;
    public override void Interact()
    {
        Debug.Log("interacting");
        DialogueManager.instance.onDialogueEnd.RemoveAllListeners();
        DialogueManager.instance.onDialogueEnd.AddListener(AfterDialogue);
        DialogueManager.instance.StartDialogue(lines, speakersIndexes, voiceLines);
    }

    void AfterDialogue()
    {
        DialogueManager.instance.onDialogueEnd.RemoveListener(AfterDialogue);
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
