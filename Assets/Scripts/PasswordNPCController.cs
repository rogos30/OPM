using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class PasswordNPCController : Interactable
{
    [SerializeField] Sprite[] clues;
    [SerializeField] ArtifactController[] clueObjects;
    [SerializeField] string requiredPassword;
    [SerializeField] int maxPasswordLength;

    [Header("Dialogue before clues")]
    [SerializeField] string[] preCluesLines;
    [SerializeField] AudioClip[] preCluesVoiceLines;
    [SerializeField] int[] preCluesSpeakersIndexes;

    [Header("Dialogue after clues")]
    [SerializeField] string[] postCluesLines;
    [SerializeField] AudioClip[] postCluesVoiceLines;
    [SerializeField] int[] postCluesSpeakersIndexes;

    [Header("Dialogue after guessing")]
    [SerializeField] string[] postPasswordLines;
    [SerializeField] AudioClip[] postPasswordVoiceLines;
    [SerializeField] int[] postPasswordSpeakersIndexes;

    string currentPassword = "";
    bool isGuessingPassword = false;

    protected override void Update()
    {
        base.Update();
        if (isGuessingPassword)
        {
            HandleInput();
        }
    }

    public override void Interact()
    {
        int cluesVisited = 0;
        foreach (var clue in clueObjects)
        {
            if (clue.wasSeen)
            {
                cluesVisited++;
            }
        }
        if (cluesVisited == clueObjects.Length)
        { //start password guessing
            DialogManager.instance.onDialogueEnd.RemoveAllListeners();
            DialogManager.instance.onDialogueEnd.AddListener(() => {
                isGuessingPassword = true;
                GameManager.instance.passwordCanvas.enabled = true;
                GameManager.instance.inGameCanvas.enabled = false;
                GameManager.instance.passwordText.text = currentPassword = "";
                for (int i = 0; i < GameManager.instance.passwordClues.Length; i++)
                {
                    if (i >= clues.Length)
                    {
                        GameManager.instance.passwordClues[i].color = new Color(1, 1, 1, 0);
                        GameManager.instance.passwordClues[i].sprite = null;
                    }
                    else
                    {
                        GameManager.instance.passwordClues[i].color = new Color(1, 1, 1, 1);
                        GameManager.instance.passwordClues[i].sprite = clues[i];
                    }
                }
            });
            DialogManager.instance.StartDialogue(postCluesLines, postCluesSpeakersIndexes, postCluesVoiceLines);
        }
        else
        { //cant start password guessing yet
            DialogManager.instance.onDialogueEnd.RemoveAllListeners();
            DialogManager.instance.StartDialogue(preCluesLines, preCluesSpeakersIndexes, preCluesVoiceLines);
        }
        
    }

    void Exit()
    {
        GameManager.instance.passwordCanvas.enabled = false;
        GameManager.instance.inGameCanvas.enabled = true;
        isGuessingPassword = false;
    }

    void FinalizeAndExit()
    {
        DialogManager.instance.onDialogueEnd.RemoveAllListeners();
        DialogManager.instance.StartDialogue(postPasswordLines, postPasswordSpeakersIndexes, postPasswordVoiceLines);
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
        Exit();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        { //check password
            if (currentPassword.Equals(requiredPassword))
            {
                FinalizeAndExit();
            }
            else
            {
                Exit();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit();
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (currentPassword.Length > 0)
            {
                currentPassword = currentPassword.Remove(currentPassword.Length - 1);
                GameManager.instance.passwordText.text = currentPassword;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Delete))
        {
            currentPassword = "";
            GameManager.instance.passwordText.text = currentPassword;
        }
        else
        {
            char letter = GetLetterPressed();
            if (letter != ' ' && currentPassword.Length < maxPasswordLength)
            {
                currentPassword += letter;
                GameManager.instance.passwordText.text = currentPassword;
            }
        }
    }

    char GetLetterPressed()
    {
        if (Input.GetKeyDown(KeyCode.A)) return 'a';
        if (Input.GetKeyDown(KeyCode.B)) return 'b';
        if (Input.GetKeyDown(KeyCode.C)) return 'c';
        if (Input.GetKeyDown(KeyCode.D)) return 'd';
        if (Input.GetKeyDown(KeyCode.E)) return 'e';
        if (Input.GetKeyDown(KeyCode.F)) return 'f';
        if (Input.GetKeyDown(KeyCode.G)) return 'g';
        if (Input.GetKeyDown(KeyCode.H)) return 'h';
        if (Input.GetKeyDown(KeyCode.I)) return 'i';
        if (Input.GetKeyDown(KeyCode.J)) return 'j';
        if (Input.GetKeyDown(KeyCode.K)) return 'k';
        if (Input.GetKeyDown(KeyCode.L)) return 'l';
        if (Input.GetKeyDown(KeyCode.M)) return 'm';
        if (Input.GetKeyDown(KeyCode.N)) return 'n';
        if (Input.GetKeyDown(KeyCode.O)) return 'o';
        if (Input.GetKeyDown(KeyCode.P)) return 'p';
        if (Input.GetKeyDown(KeyCode.Q)) return 'q';
        if (Input.GetKeyDown(KeyCode.R)) return 'r';
        if (Input.GetKeyDown(KeyCode.S)) return 's';
        if (Input.GetKeyDown(KeyCode.T)) return 't';
        if (Input.GetKeyDown(KeyCode.U)) return 'u';
        if (Input.GetKeyDown(KeyCode.V)) return 'v';
        if (Input.GetKeyDown(KeyCode.W)) return 'w';
        if (Input.GetKeyDown(KeyCode.X)) return 'x';
        if (Input.GetKeyDown(KeyCode.Y)) return 'y';
        if (Input.GetKeyDown(KeyCode.Z)) return 'z';
        return ' ';
    } 
}
