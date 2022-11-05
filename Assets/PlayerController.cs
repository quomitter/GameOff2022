using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public Animator anim;
    public Rigidbody2D rb;
    public Transform groundCheck;
    public Transform punchCheck;
    public Transform kickCheck;
    public LayerMask whatIsGround;
    public LayerMask whatIsEnemy;
    public Sprite[] slantedHealthBar;
    public float attackRange = 0.5f;
    public SpriteRenderer playerSprite;
    public bool isGrounded;
    public int moveSpeed;
    public float moveDampener;
    public int jumpForce;
    public bool facingRight = true;
    private EnemyHealthBar enemyHealthBar;
    private PlayerHealthBar playerHealthBar;
    private EnemyController enemyController;
    public bool playerHit;
    public AudioSource audioSource;
    public AudioClip punchSound;
    public AudioClip kickSound;
    public bool isBlocking;
    public Canvas winOrLoseCanvas; 
    public TMP_Text winOrLose;
    public Button tryAgain;
    public bool gameIsActive; 

    // Start is called before the first frame update
    void Start()
    {
        enemyHealthBar = FindObjectOfType<EnemyHealthBar>();
        playerHealthBar = FindObjectOfType<PlayerHealthBar>();
        enemyController = FindObjectOfType<EnemyController>();
        audioSource = GetComponent<AudioSource>();
        winOrLoseCanvas.enabled = false;
        gameIsActive = true; 
    }

    // Update is called once per frame
    void Update()
    {
        enemyController.enemyHit = false;
        if (playerHealthBar.playerHealthLevel <= 0)
        {
            winOrLoseCanvas.enabled = true; 
            winOrLose.text = "You Lost";
            gameIsActive = false;
        }
        if (enemyHealthBar.enemyHealthLevel <= 0)
        {
            winOrLoseCanvas.enabled = true; 
            winOrLose.text = "You Won";
            gameIsActive = false;
        }

        if (gameIsActive)
        {



            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, whatIsGround);
            if(Input.GetButtonDown("Jump") && (isGrounded))
            {
                PlayerJump(); 
            }
            if ((Input.GetKeyDown(KeyCode.W) && isGrounded))
            {
                PlayerJump();
            }
            if (Input.GetKeyUp(KeyCode.W))
            {

            }
            switch(Input.GetAxis("Horizontal"))
            {
                case 0:
                    anim.SetBool("isWalking", false);
                    break; 
                case float i when i > 0 && i <= 1:
                    anim.SetBool("isWalking", true);
                    if (!facingRight)
                        FlipX();
                    rb.velocity += new Vector2(1 * moveDampener, 0);
                break;
                case float i when i < 0 && i >= -1:
                    anim.SetBool("isWalking", true);
                    if (facingRight)
                        FlipX();
                    rb.velocity += new Vector2(-1 * moveDampener, 0);
                    break; 
            }

            switch (Input.GetAxis("Vertical"))
            {
                case 0:
                    anim.SetBool("isCrouching", false);
                    break;
                case float i when i > 0 && i <= 1:
                    PlayerJump();
                    break; 
                case float i when i < 0 && i >= -1:
                    anim.SetBool("isCrouching", true);
                    break; 
       
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                anim.SetBool("isCrouching", true);
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                anim.SetBool("isCrouching", false);
            }
            if (Input.GetKey(KeyCode.D))
            {
                anim.SetBool("isWalking", true);
                if (!facingRight)
                    FlipX();
                rb.velocity += new Vector2(1 * moveDampener, 0);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                anim.SetBool("isWalking", false);

            }
            if (Input.GetKey(KeyCode.A))
            {
                anim.SetBool("isWalking", true);
                if (facingRight)
                    FlipX();
                rb.velocity += new Vector2(-1 * moveDampener, 0);
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                anim.SetBool("isWalking", false);

            }
            if (Input.GetButtonDown("Fire1"))
            {
                PunchEnemy(); 
            }
            if (Input.GetButtonUp("Fire1"))
            {
                anim.SetBool("isPunching", false);
            }

            if (Input.GetKeyDown(KeyCode.Space) && !isBlocking)
            {  
                PunchEnemy();
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                anim.SetBool("isPunching", false);

            }
            if (Input.GetButtonDown("Fire2"))
            {
                KickEnemy();
            }
            if (Input.GetButtonUp("Fire2"))
            {
                anim.SetBool("isKicking", false);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && !isBlocking)
            {  
                KickEnemy();
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                anim.SetBool("isKicking", false);
            }

            if (Input.GetButtonDown("Fire3"))
            {
                anim.SetBool("isBlocking", true);
                isBlocking = true;
            }
            if (Input.GetButtonUp("Fire3"))
            {
                anim.SetBool("isBlocking", false);
                isBlocking = false;
            }
            if (Input.GetKey(KeyCode.Tab))
            {
                anim.SetBool("isBlocking", true);
                isBlocking = true;
            }
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                anim.SetBool("isBlocking", false);
                isBlocking = false;
            }
        }
    }

    void FlipX()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void PunchEnemy()
    {

        anim.SetBool("isPunching", true);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(punchCheck.position, attackRange, whatIsEnemy);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemyController.enemyHit = true;
            if (!enemyController.isBlocking)
                enemyHealthBar.enemyHealthLevel -= 1;
            audioSource.PlayOneShot(punchSound);
        }

    }

    void KickEnemy()
    {

        anim.SetBool("isKicking", true);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(kickCheck.position, attackRange, whatIsEnemy);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemyController.enemyHit = true;
            if (!enemyController.isBlocking)
                enemyHealthBar.enemyHealthLevel -= 1;
            audioSource.PlayOneShot(kickSound);
        }

    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }

    void PlayerJump()
    {
        if(isGrounded)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Force);
    }
}
