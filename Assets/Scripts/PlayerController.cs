using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float moveSpeed = 4;
    float timeToFight;
    Animator animator;
    bool isFacingRight = false, isMoving = false;

    [SerializeField] GameObject interactionPrompt;
    void Start()
    {
        animator = GetComponent<Animator>();
        timeToFight = Random.Range(25, 40);
        interactionPrompt.SetActive(false);
    }

    void Update()
    {
        if (!GameManager.instance.pauseCanvas.enabled && !DialogManager.instance.dialogueCanvas.enabled)
        {
            isMoving = false;
            HandleInput();
            if (isMoving)
            {
                timeToFight -= Time.deltaTime;
            }
            if (timeToFight <= 0)
            {
                timeToFight = Random.Range(25, 40);
                int[] playables = new int[BattleManager.instance.playableCharacters.Count];
                for (int i = 0; i < playables.Length; i++)
                {
                    playables[i] = i;
                }
                bool[] enemiesFirstDraw = new bool[4];
                int amountOfEnemies = 0;
                for (int i = 0; i < 4; i++)
                {
                    enemiesFirstDraw[i] = Random.Range(0, 2) == 1;
                    if (enemiesFirstDraw[i])
                    {
                        amountOfEnemies++;
                    }
                }
                int[] enemies = new int[amountOfEnemies];
                int j = 0;
                for (int i = 0;i < 4; i++)
                {
                    if (enemiesFirstDraw[i])
                    {
                        enemies[j++] = i;
                    }
                }
                BattleManager.instance.InitiateBattle(playables, enemies);
            }
        }
    }

    void HandleInput()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            MoveHorizontal(true);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            MoveHorizontal(false);
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            MoveVertical(true);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            MoveVertical(false);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Inventory.Instance.Money += 1000;
            Debug.Log(Inventory.Instance.Money);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            for (int i = 0; i < BattleManager.instance.playableCharacters.Count; i++)
            {
                BattleManager.instance.playableCharacters[i].HandleLevel(5000);
            }
        }
        if ( Input.GetKeyDown(KeyCode.L))
        {

            int[] playables = new int[BattleManager.instance.playableCharacters.Count];
            for (int i = 0; i < playables.Length; i++)
            {
                playables[i] = i;
            }
            int[] enemies = new int[1];
            enemies[0] = 6;
            BattleManager.instance.InitiateBattle(playables, enemies);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.instance.canPause)
            {
                GameManager.instance.Pause();
            }
        }
        if (Input.GetKey(KeyCode.F7))  
        {
            animator.SetBool("isKaboom", true);
            StartCoroutine(Disable());
        }
        if (!Input.anyKey)
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
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            interactionPrompt.SetActive(false);
        }
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(0.4f);
        this.gameObject.SetActive(false);
    }
}
