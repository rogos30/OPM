using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFollowerController : Interactable
{
    [SerializeField] private Transform target;
    [Range(0.5f, 10f)][SerializeField] private float moveSpeed;
    public float distanceFromTarget = 1.5f;
    public float delay = 0.5f;

    Animator animator;
    bool isFacingRight = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        transform.position = target.position;   
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(MoveDelayed(target));
    }

    IEnumerator MoveDelayed(Transform target)
    {
        yield return new WaitForSeconds(delay);
        if (Vector2.Distance(transform.position, target.position) >= distanceFromTarget)
        {
            Vector2 difference = target.position - transform.position;
            double angle = Math.Abs(Math.Atan(difference.x / difference.y) * 180/Math.PI);
            //Debug.Log(angle);
            if (angle > 45)
            {
                MoveHorizontal(difference.x >= 0);
            }
            else
            {
                MoveVertical(difference.y >= 0);
            }
            if (Vector2.Distance(transform.position, target.position) > 5)
            {
                transform.position = target.position;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            }
        }
        else
        {
            animator.SetInteger("isWalking", 0);
        }
    }
    void MoveHorizontal(bool right)
    {
        if (right)
        {
            if (!isFacingRight)
            {
                Flip();
            }
        }
        else
        {

            if (isFacingRight)
            {
                Flip();
            }
        }
        animator.SetInteger("isWalking", 3);
    }
    void MoveVertical(bool up)
    {
        if (up)
        {
            animator.SetInteger("isWalking", 1);
        }
        else
        {
            animator.SetInteger("isWalking", 2);
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public override void Interact()
    {
        return;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("VisionCone"))
        {
            //Debug.Log("AAAAAAAAA");
            other.GetComponentInParent<PatrolNPCController>().CheckIfPlayerInSight();
        }
        else if (other.CompareTag("PursuitKillZone"))
        {
            other.GetComponentInParent<PatrolNPCController>().killZoneEngaged = true;
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
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("VisionCone"))
        {
            //Debug.Log("BBBBBBBBB");
            other.GetComponentInParent<PatrolNPCController>().PlayableLeaveSight();
        }
    }
}
