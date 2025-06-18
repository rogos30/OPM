using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatrolNPCController : Interactable
{
    [SerializeField] private Slider currentAwarenessSlider;
    private const float moveSpeed = 1.5f;
    private const float waitTimeAtPatrolPoint = 5f;
    private float currentWaitTime = 0;
    private const float timeBetweenAwarenessUpdates = 0.05f;
    private float currentTimeBetweenAwarenessUpdates = 0;
    private const float cooldownTime = 3f;
    private float currentCooldownTime = 0;
    private float awareness = 0;
    private int minAwareness = 0;
    [SerializeField] private GameObject[] patrolPoints;
    [SerializeField] private GameObject visionCone;
    [SerializeField] private GameObject player;
    private int currentPatrolPoint = 0;
    private bool canMove = true;
    private bool hasSetDirection = true;
    private bool isInPatrol = false;
    public bool isPlayerInSight = false;

    Animator animator;
    bool isFacingRight = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentAwarenessSlider.gameObject.SetActive(false);
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
        if (isPlayerInSight)
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
        if (!isPlayerInSight)
        {
            currentWaitTime += Time.deltaTime;
        }
        if (currentWaitTime >= waitTimeAtPatrolPoint)
        {
            isInPatrol = false;
            canMove = true;
            currentWaitTime = 0;
            hasSetDirection = false;
        }
    }

    public void CheckIfPlayerInSight()
    {
        isPlayerInSight = true;
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
        if (awareness > minAwareness && currentTimeBetweenAwarenessUpdates >= timeBetweenAwarenessUpdates && currentCooldownTime >= cooldownTime)
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
            awareness += (GameManager.instance.difficulty+1);
            currentAwarenessSlider.value = awareness;
            currentTimeBetweenAwarenessUpdates = 0;
        }
        if (awareness >= 100)
        {
            Debug.Log("Spotted! Game over!");
        }
        else if (awareness >= 75)
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

    void ShowDebugMessages()
    {
        if (timeBetweenAwarenessUpdates <= currentTimeBetweenAwarenessUpdates)
        {
            Debug.Log("skradanka: ");
            Debug.Log("isPlayerInSight " + isPlayerInSight);
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
