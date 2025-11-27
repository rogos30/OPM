using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : Interactable
{
    public bool isAutomatic;
    [SerializeField] Transform destination;
    [SerializeField] Transform player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isAutomatic)
            {
                Interact();
            }
            else
            {
                playerNearby = true;
            } 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }

    public override void Interact()
    {
        StartCoroutine(GameManager.instance.FadeToBlack());
        StartCoroutine(TeleportDelayed());
    }

    IEnumerator TeleportDelayed()
    {
        yield return new WaitForSeconds(0.3f);
        player.position = destination.position;
    }
}
