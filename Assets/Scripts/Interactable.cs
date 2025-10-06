using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    bool playerNearby = false;
    [SerializeField] protected bool interactionProgressesStory;
    [SerializeField] protected bool interactionSavesGame;
    [SerializeField] protected bool interactionBlocksSavingGame;
    public int appearanceAtQuest;
    public int disappearanceAtQuest;

    protected virtual void Update()
    {
        if (canInteract())
        {
            Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }

    protected bool canInteract()
    {
        return (playerNearby && GameManager.instance.inGameCanvas.enabled && !DialogManager.instance.dialogueCanvas.enabled
            && !DialogManager.instance.gameInfoCanvas.enabled && !GameManager.instance.artifactCanvas.enabled
            && Input.GetKeyDown(KeyCode.E) && !GameManager.instance.pauseCanvas.enabled);
    }
    public abstract void Interact();
}
