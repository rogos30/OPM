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

    private void Update()
    {
        if (playerNearby)
        {
            if (Input.GetKeyDown(KeyCode.E) && !GameManager.instance.pauseCanvas.enabled)
            {
                Interact();
            }
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
    public abstract void Interact();
}
