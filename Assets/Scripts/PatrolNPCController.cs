using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PatrolNPCController : Interactable
{
    [SerializeField] private Slider currentAwarenessSlider;
    float currentWaitTime = 0;
    const float timeBetweenAwarenessUpdates = 0.05f;
    float currentTimeBetweenAwarenessUpdates = 0;
    float currentCooldownTime = 0;
    int awareness = 0;
    int minAwareness = 0;
    int awarenessIncrease;
    [SerializeField] private GameObject[] patrolPoints;
    [SerializeField] private GameObject visionCone;
    [SerializeField] private GameObject pursuitKillZone;
    [Range(0.5f, 10f)][SerializeField] float moveSpeed;
    [Range(0, 10f)][SerializeField] float waitTimeAtPatrolPoint;
    [Range(0, 10f)][SerializeField] float awarenessCooldownTime;
    [Range(5f, 30f)][SerializeField] float distanceToEscape;
    [SerializeField] GameObject player;
    [SerializeField] bool loopsPath;
    [SerializeField] bool hasKillZone;
    [SerializeField] bool playerInSightExtendsPatrol;
    [SerializeField] bool hasAwarenessThresholds;
    int currentPatrolPoint = 0;
    bool canMove = true;
    bool hasSetDirection = true;
    bool isInPatrol = false;
    bool isPlayerCaught = false;
    bool isSomeoneInSight = false;
    public bool killZoneEngaged = false;
    int playablesInSight = 0;
    int[] visionConeScales = { 2, 3, 4, 5 };
    float distance;
    float playerSpeed;
    [SerializeField] AudioClip[] onFailVoiceLine;

    Animator animator;
    bool isFacingRight = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentAwarenessSlider.gameObject.SetActive(false);
        awarenessIncrease = GameManager.instance.difficulty;
        if (!loopsPath)
        {
            visionCone.SetActive(false);
        }
        if (hasKillZone)
        {
            pursuitKillZone.SetActive(true);
        }
        if (player != null)
        {
            playerSpeed = player.GetComponent<PlayerController>().moveSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentTimeBetweenAwarenessUpdates += Time.deltaTime;
        if (canMove)
        {
            Move();
        }
        else if (isInPatrol)
        {
            HandlePatrol();
        }
        if (isSomeoneInSight)
        {
            HandlePlayerInSight();
        }
        else
        {
            HandlePlayerOutOfSight();
        }
        if (!loopsPath)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance >= distanceToEscape && !isPlayerCaught)
            {
                GameOver();
                isPlayerCaught = true;
            }
            else if (distanceToEscape - distance < 5)
            {
                GameManager.instance.targetIsEscapingText.gameObject.SetActive(true);
            }
            else
            {
                GameManager.instance.targetIsEscapingText.gameObject.SetActive(false);
            }
        }
        //ShowDebugMessages();
    }

    void HandlePatrol()
    {
        animator.SetInteger("isWalking", 0);
        if (!playerInSightExtendsPatrol || !isSomeoneInSight)
        {
            currentWaitTime += Time.deltaTime;
        }
        if (currentWaitTime >= waitTimeAtPatrolPoint && !isPlayerCaught)
        {
            isInPatrol = false;
            canMove = true;
            currentWaitTime = 0;
            hasSetDirection = false;
        }
    }

    public void UpdateDifficulty(int difficulty)
    {
        Vector3 theScale = transform.localScale;
        theScale.x = visionConeScales[difficulty];
        theScale.y = visionConeScales[difficulty];
        visionCone.transform.localScale = theScale;
        awarenessIncrease = difficulty + 1;
    }

    public void CheckIfPlayerInSight()
    {
        playablesInSight++;
        isSomeoneInSight = true;
    }

    public void PlayableLeaveSight()
    {
        playablesInSight--;
        if (playablesInSight == 0)
        {
            isSomeoneInSight = false;
        }
    }

    void Move()
    {
        if (Vector2.Distance(patrolPoints[currentPatrolPoint].transform.position, transform.position) < 0.1f)
        {
            if (!loopsPath)
            {
                if (currentPatrolPoint + 1 == patrolPoints.Length)
                {
                    if (interactionProgressesStory)
                    {
                        StoryManager.instance.ProgressStory();
                    }
                    if (interactionSavesGame)
                    {
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
            currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
            isInPatrol = true;
            canMove = false;
        }
        if (canMove)
        {
            float speed;
            if (hasKillZone)
            { //chases player
                speed = distance > 10 ? playerSpeed + 1 : moveSpeed;
            }
            else
            { //runs from player
                speed = distance < 4 ? playerSpeed + 1 : moveSpeed;
            } 
            if (killZoneEngaged)
            {
                speed = 10;
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPatrolPoint].transform.position, speed * Time.deltaTime);
            }
            if (!hasSetDirection)
            {
                if (Math.Abs(patrolPoints[currentPatrolPoint].transform.position.y - transform.position.y) <= 0.2f)
                {
                    Debug.Log("horizontal");
                    MoveHorizontal(patrolPoints[currentPatrolPoint].transform.position.x >= transform.position.x);
                }
                else
                {
                    Debug.Log("vertical");
                    MoveVertical(patrolPoints[currentPatrolPoint].transform.position.y >= transform.position.y);
                }
                hasSetDirection = true;
            }
        }
    }

    void MoveHorizontal(bool right)
    {
        if (right)
        {
            visionCone.transform.eulerAngles = new Vector3(0, 0, -90);
            pursuitKillZone.transform.eulerAngles = new Vector3(0, 0, -90);
            if (!isFacingRight)
            {
                Flip();
            }
        }
        else
        {

            visionCone.transform.eulerAngles = new Vector3(0, 0, 90);
            pursuitKillZone.transform.eulerAngles = new Vector3(0, 0, 90);
            if (isFacingRight)
            {
                Flip();
            }
        }
        animator.SetInteger("isWalking", 3);
    }
    void MoveVertical(bool up)
    {
        var rotation = visionCone.transform.rotation;
        if (up)
        {
            rotation.z = 180;
            animator.SetInteger("isWalking", 1);
        }
        else
        {
            rotation.z = 0;
            animator.SetInteger("isWalking", 2);
        }
        visionCone.transform.rotation = rotation;
        pursuitKillZone.transform.rotation = rotation;
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        currentAwarenessSlider.transform.localScale = theScale;
    }

    void HandlePlayerOutOfSight()
    {
        currentCooldownTime += Time.deltaTime;
        if (awareness > minAwareness && currentTimeBetweenAwarenessUpdates >= timeBetweenAwarenessUpdates && currentCooldownTime >= awarenessCooldownTime)
        {
            awareness--;
            currentTimeBetweenAwarenessUpdates = 0;
            currentAwarenessSlider.value = awareness;
        }
        if (awareness == 0)
        {
            currentAwarenessSlider.gameObject.SetActive(false);
        }
    }

    void HandlePlayerInSight()
    {
        currentCooldownTime = 0;
        if (currentTimeBetweenAwarenessUpdates >= timeBetweenAwarenessUpdates)
        {
            currentAwarenessSlider.gameObject.SetActive(true);
            awareness += awarenessIncrease;
            currentAwarenessSlider.value = awareness;
            currentTimeBetweenAwarenessUpdates = 0;
        }
        if (awareness >= 100 && !isPlayerCaught)
        {
            GameOver();
        }
        else if (hasAwarenessThresholds)
        {
            if (awareness >= 75)
            {
                minAwareness = 75;
            }
            else if (awareness >= 50)
            {
                minAwareness = 50;
            }
            else if (awareness >= 25)
            {
                minAwareness = 25;
            }
        }
    }

    public void GameOver()
    {
        isPlayerCaught = true;
        canMove = false;
        string[] lines = {
                        "Niech to!" };
        int[] speakerIndexes = { 0 };
        DialogManager.instance.StartDialogue(lines, speakerIndexes, onFailVoiceLine);
        DialogManager.instance.onDialogueEnd.AddListener(() => {
            SceneManager.LoadScene("gameOver");
        });
    }

    void ShowDebugMessages()
    {
        if (timeBetweenAwarenessUpdates <= currentTimeBetweenAwarenessUpdates)
        {
            Debug.Log("skradanka: ");
            Debug.Log("isSomeoneInSight " + isSomeoneInSight);
            Debug.Log("isInPatrol " + isInPatrol);
            Debug.Log("canMove " + canMove);
            Debug.Log("currentWaitTime " + currentWaitTime);
            Debug.Log("awareness " + awareness);
            Debug.Log("minAwareness " + minAwareness);
        }
    }

    public override void Interact()
    {
        return;
    }
}
