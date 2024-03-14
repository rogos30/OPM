using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;
    public UnityEvent onDialogueEnd;
    [SerializeField] public Canvas dialogueCanvas;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] TMP_Text speakersName;
    [SerializeField] Image speakerSprite;
    [SerializeField] public Sprite[] speakerSprites;
    [SerializeField] string[] speakerNames;
    const float lettersPerSecond = 40f;
    string[] lines;
    int[] speakersIndexes;
    int currentIndex;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        dialogueCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueCanvas.enabled)
        {
            HandleInput();
        }
    }

    void HandleInput()
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
    public void StartDialogue(string[] lines, int[] speakersIndexes)
    {
        currentIndex = -1;
        dialogueCanvas.enabled = true;
        this.lines = lines;
        this.speakersIndexes = speakersIndexes;
        NextLine();
    }
    void EndDialogue()
    {
        dialogueCanvas.enabled = false;
        onDialogueEnd.Invoke();
    }

    void NextLine()
    {
        if (currentIndex < lines.Length - 1)
        {
            currentIndex++;
            speakerSprite.sprite = speakerSprites[speakersIndexes[currentIndex]];
            speakersName.text = speakerNames[speakersIndexes[currentIndex]];
            dialogueText.text = "";
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
}
