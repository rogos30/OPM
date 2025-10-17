using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PatrolNPCController : Interactable
{
    [SerializeField] private Slider currentAwarenessSlider;
    private float currentWaitTime = 0;
    private const float timeBetweenAwarenessUpdates = 0.05f;
    private float currentTimeBetweenAwarenessUpdates = 0;
    private float currentCooldownTime = 0;
    private int awareness = 0;
    private int minAwareness = 0;
    private int awarenessIncrease;
    [SerializeField] private GameObject[] patrolPoints;
    [SerializeField] private GameObject visionCone;
    [Range(0.5f, 10f)][SerializeField] private float moveSpeed;
    [Range(0.5f, 10f)][SerializeField] private float waitTimeAtPatrolPoint;
    [Range(0.5f, 10f)][SerializeField] private float awarenessCooldownTime;
    [SerializeField] private bool playerInSightExtendsPatrol;
    [SerializeField] private bool hasAwarenessThresholds;
    private int currentPatrolPoint = 0;
    private bool canMove = true;
    private bool hasSetDirection = true;
    private bool isInPatrol = false;
    private bool isPlayerCaught = false;
    public bool isSomeoneInSight = false;
    int playablesInSight = 0;
    int[] visionConeScales = { 2, 3, 4, 5 };
    [SerializeField] AudioClip[] onSpottedVoiceLine;

    Animator animator;
    bool isFacingRight = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentAwarenessSlider.gameObject.SetActive(false);
        awarenessIncrease = GameManager.instance.difficulty;
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
            currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
            isInPatrol = true;
            canMove = false;
        }
        if (canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPatrolPoint].transform.position, moveSpeed * Time.deltaTime);
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
            if (!isFacingRight)
            {
                Flip();
            }
        }
        else
        {

            visionCone.transform.eulerAngles = new Vector3(0, 0, 90);
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

    void GameOver()
    {
        isPlayerCaught = true;
        canMove = false;
        string[] lines = {
                        "Ups" };
        int[] speakerIndexes = { 0 };
        DialogManager.instance.StartDialogue(lines, speakerIndexes, onSpottedVoiceLine);
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
