using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 4;
    float timeToFight;
    Animator animator;
    bool isFacingRight = false, isMoving = false;
    int currentArea = 0;

    [SerializeField] GameObject interactionPrompt;
    void Start()
    {
        animator = GetComponent<Animator>();
        ResetTimeToRandomEcounter();
        interactionPrompt.SetActive(false);
    }

    void Update()
    {
        if (GameManager.instance.inGameCanvas.enabled && !DialogManager.instance.dialogueCanvas.enabled && !DialogManager.instance.gameInfoCanvas.enabled
            && !GameManager.instance.artifactCanvas.enabled && !GameManager.instance.passwordCanvas.enabled)
        {
            isMoving = false;
            HandleInput();
            if (isMoving)
            {
                timeToFight -= Time.deltaTime;
            }
            if (timeToFight <= 0)
            {
                ResetTimeToRandomEcounter();
                InitiateRandomEncounter();
            }
        }
        else
        {
            animator.SetInteger("isWalking", 0);
        }
    }

    void HandleInput()
    {
        bool pressedMovementKey = false;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            pressedMovementKey = true;
            MoveHorizontal(true);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            pressedMovementKey = true;
            MoveHorizontal(false);
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            pressedMovementKey = true;
            MoveVertical(true);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            pressedMovementKey = true;
            MoveVertical(false);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Inventory.Instance.Money += 1000;
            Debug.Log(Inventory.Instance.Money);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            for (int i = 0; i < BattleManager.instance.currentPartyCharacters.Count; i++)
            {
                int index = BattleManager.instance.currentPartyCharacters[i];
                BattleManager.instance.playableCharacters[index].HandleLevel(5000);
            }
        }
        /*if ( Input.GetKeyDown(KeyCode.L))
        {
            int[] playables = new int[BattleManager.instance.currentPartyCharacters.Count];
            for (int i = 0; i < playables.Length; i++)
            {
                playables[i] = i;
            }
            int[] enemies = { 6 };
            BattleManager.instance.InitiateBattle(playables, enemies);
        }*/
        if (Input.GetKeyDown(KeyCode.L))
        {
            StoryManager.instance.ProgressStory();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.instance.canPause)
            {
                GameManager.instance.Pause();
            }
        }
        if (!pressedMovementKey)
        {
            animator.SetInteger("isWalking", 0);
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        interactionPrompt.transform.localScale = theScale;
    }

    void MoveHorizontal(bool right)
    {
        if (right)
        {
            if (!isFacingRight)
            {
                Flip();
            }
            transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        }
        else
        {
            if (isFacingRight)
            {
                Flip();
            }
            transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        }
        animator.SetInteger("isWalking", 3);
        isMoving = true;
    }

    void MoveVertical(bool up)
    {
        if (up)
        {
            transform.Translate(0.0f, moveSpeed * Time.deltaTime, 0.0f, Space.World);
            animator.SetInteger("isWalking", 1);
        }
        else
        {
            transform.Translate(0.0f, -moveSpeed * Time.deltaTime, 0.0f, Space.World);
            animator.SetInteger("isWalking", 2);
        }
        isMoving = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            interactionPrompt.SetActive(true);
        }
        else if (other.CompareTag("Marlboro"))
        {
            StoryManager.instance.CollectMarlboro();
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("PonySticker"))
        {
            StoryManager.instance.CollectPonySticker();
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("VisionCone"))
        {
            //Debug.Log("AAAAAAAAA");
            other.GetComponentInParent<PatrolNPCController>().CheckIfPlayerInSight();
        }
        else if (other.CompareTag("Trigger"))
        {
            other.GetComponent<Interactable>().Interact();
        }
        else if (other.CompareTag("PursuitKillZone"))
        {
            other.GetComponentInParent<PatrolNPCController>().killZoneEngaged = true;
        }
        else if (other.CompareTag("PursuitNPC"))
        {
            other.GetComponentInParent<PatrolNPCController>().GameOver();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            interactionPrompt.SetActive(false);
        }
        else if (other.CompareTag("VisionCone"))
        {
            //Debug.Log("BBBBBBBBB");
            other.GetComponentInParent<PatrolNPCController>().PlayableLeaveSight();
        }
    }

    void ResetTimeToRandomEcounter()
    {
        timeToFight = Random.Range(25, 40);
        //timeToFight = Random.Range(5, 10);
    }

    void InitiateRandomEncounter()
    {
        int[] playables = new int[BattleManager.instance.currentPartyCharacters.Count];
        int[,] enemyConfigurations = { { 0, 0, 0, 1 }, { 0, 0, 1, 0 }, { 0, 0, 1, 1 }, { 0, 1, 0, 0 }, { 0, 1, 0, 1 }, { 0, 1, 1, 0 }, { 0, 1, 1, 1 },
                                        { 1, 0, 0, 0 }, { 1, 0, 0, 1 }, { 1, 0, 1, 0 }, { 1, 0, 1, 1 }, { 1, 1, 0, 0 }, { 1, 1, 0, 1 }, { 1, 1, 1, 0 }, { 1, 1, 1, 1 } };
        int enemySet = Random.Range(0, 15);
        for (int i = 0; i < playables.Length; i++)
        {
            playables[i] = i;
        }
        bool[] enemiesFirstDraw = new bool[4];
        int amountOfEnemies = 0;
        for (int i = 0; i < 4; i++)
        {
            enemiesFirstDraw[i] = enemyConfigurations[enemySet,i] == 1;
            if (enemiesFirstDraw[i])
            {
                amountOfEnemies++;
            }
        }
        int[] enemies = new int[amountOfEnemies];
        int j = 0;
        for (int i = 0; i < 4; i++)
        {
            if (enemiesFirstDraw[i])
            {
                enemies[j++] = BattleManager.instance.randomEncounterEnemyIndexes[currentArea, i];
            }
        }
        BattleManager.instance.InitiateBattle(playables, enemies, 0, true);
    }
}
