using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactController : Interactable
{
    [SerializeField] Sprite artifact;
    [SerializeField] string[] lines;
    [SerializeField] AudioClip[] voiceLines;
    [SerializeField] int[] speakersIndexes;
    bool canLeave = false;
    public bool wasSeen = false;
    [SerializeField] bool disappearAfterInteraction;
    public string artifactName;
    public string description;

    protected override void Update()
    {
        base.Update();
        if (canLeave)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                Exit();
            }
        }
    }

    public override void Interact()
    {
        if (canInteract())
        {
            canLeave = false;
            GameManager.instance.artifactCanvas.enabled = true;
            GameManager.instance.artifactCanvas.GetComponentInChildren<Image>().sprite = artifact;
            StartCoroutine(startDialogueDelayed());
        }
    }

    void Exit()
    {
        GameManager.instance.artifactCanvas.enabled = false;
        wasSeen = true;
        if (disappearAfterInteraction)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator startDialogueDelayed()
    {
        yield return new WaitForSeconds(1.25f);
        DialogManager.instance.onDialogueEnd.RemoveAllListeners();
        DialogManager.instance.onDialogueEnd.AddListener(() => { StartCoroutine(allowToLeave()); });
        DialogManager.instance.StartDialogue(lines, speakersIndexes, voiceLines);
    }

    IEnumerator allowToLeave()
    {
        yield return new WaitForSeconds(0.25f);
        canLeave = true;
    }
}
