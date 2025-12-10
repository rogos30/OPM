using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public UnityEvent onDialogueEnd;
    public UnityEvent onGameInfoEnd;
    [SerializeField] public Canvas dialogueCanvas;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] public Canvas gameInfoCanvas;
    [SerializeField] TMP_Text gameInfoText;
    [SerializeField] TMP_Text speakersName;
    [SerializeField] Image speakerSprite;
    [SerializeField] public Sprite[] speakerSprites;
    [SerializeField] string[] speakerNames;
    const float lettersPerSecond = 40f;
    string[] lines;
    int[] speakersIndexes;
    AudioClip[] voiceLines;
    int currentIndex;
    AudioSource sfxSource;
    public AudioMixerGroup sfxMixerGroup;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        dialogueCanvas.enabled = false;
        gameInfoCanvas.enabled = false;
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.outputAudioMixerGroup = sfxMixerGroup;
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueCanvas.enabled)
        {
            HandleDialogueInput();
        }
        else if (gameInfoCanvas.enabled)
        {
            HandleGameInfoInput();
        }
    }

    void HandleDialogueInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dialogueText.text == lines[currentIndex])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = lines[currentIndex];
            }
        }
    }
    public void StartDialogue(string[] lines, int[] speakersIndexes, AudioClip[] voiceLines)
    {
        if (dialogueCanvas.enabled) return;
        currentIndex = -1;
        dialogueCanvas.enabled = true;
        this.lines = lines;
        this.speakersIndexes = speakersIndexes;
        this.voiceLines = voiceLines;
        NextLine();
    }
    
    public void EndDialogue()
    {
        sfxSource.Stop();
        dialogueCanvas.enabled = false;
        onDialogueEnd.Invoke();
    }

    void NextLine()
    {
        if (currentIndex < lines.Length - 1)
        {
            sfxSource.Stop();
            currentIndex++;
            speakerSprite.sprite = speakerSprites[speakersIndexes[currentIndex]];
            speakersName.text = speakerNames[speakersIndexes[currentIndex]];
            dialogueText.text = "";
            if (currentIndex < voiceLines.Length)
            {
                sfxSource.clip = voiceLines[currentIndex];
                sfxSource.loop = false;
                sfxSource.Play();
            }
            StartCoroutine(TypeDialogue());
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeDialogue()
    {
        dialogueText.text = "";
        foreach (var letter in lines[currentIndex].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
    }



    void HandleGameInfoInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameInfoNextLine();
        }
    }
    public void StartGameInfo(string[] lines)
    {
        if (lines.Length == 0)
        {
            Debug.Log("no lines");
            onGameInfoEnd.Invoke();
            return;
        }
        currentIndex = -1;
        gameInfoCanvas.enabled = true;
        this.lines = lines;
        GameInfoNextLine();
    }
    void GameInfoNextLine()
    {
        if (currentIndex < lines.Length - 1)
        {
            currentIndex++;
            gameInfoText.text = lines[currentIndex];
        }
        else
        {
            EndGameInfo();
        }
    }
    void EndGameInfo()
    {
        gameInfoCanvas.enabled = false;
        onGameInfoEnd.Invoke();
    }
}
