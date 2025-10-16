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
        if (lines.Length > 0)
        {
            DialogManager.instance.onDialogueEnd.RemoveAllListeners();
            DialogManager.instance.onDialogueEnd.AddListener(AfterDialogue);
            DialogManager.instance.StartDialogue(lines, speakersIndexes, voiceLines);
        }
    }

    void AfterDialogue()
    {
        DialogManager.instance.onDialogueEnd.RemoveListener(AfterDialogue);
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
