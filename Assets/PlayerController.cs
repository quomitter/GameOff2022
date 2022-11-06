using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject fireball; 
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
    public AudioClip enemyFireballSound; 
    public bool isBlocking;
    public Canvas winOrLoseCanvas; 
    public TMP_Text winOrLose;
    public Button tryAgain;
    public bool gameIsActive;
    public float leftButtonTimer;
    public float rightButtonTimer; 

    // Start is called before the first frame update
    void Start()
    {
        enemyHealthBar = FindObjectOfType<EnemyHealthBar>();
        playerHealthBar = FindObjectOfType<PlayerHealthBar>();
        enemyController = FindObjectOfType<EnemyController>();
        audioSource = GetComponent<AudioSource>();
        winOrLoseCanvas.enabled = false;
        gameIsActive = true; 
        leftButtonTimer = 0;
        rightButtonTimer = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        leftButtonTimer -= Time.deltaTime;
        rightButtonTimer -= Time.deltaTime;

        enemyController.enemyHit = false;
        if (playerHealthBar.playerHealthLevel <= 0)
        {
            winOrLoseCanvas.enabled = true; 
            winOrLose.text = "You Lost";
            gameIsActive = false;
            playerHealthBar.playerSpriteRenderer.sprite = playerHealthBar.playerHealthBar[0];
        }
        if (enemyHealthBar.enemyHealthLevel <= 0)
        {
            winOrLoseCanvas.enabled = true; 
            winOrLose.text = "You Won";
            gameIsActive = false;
            enemyHealthBar.enemySpriteRenderer.sprite = enemyHealthBar.enemyHealthBar[0];
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
                    rightButtonTimer = 2.0f; 
                    break;
                case float i when i < 0 && i >= -1:
                    anim.SetBool("isWalking", true);
                    if (facingRight)
                        FlipX();
                    rb.velocity += new Vector2(-1 * moveDampener, 0);
                    leftButtonTimer = 2.0f; 
                    break; 
            }
            if((rightButtonTimer > 0 && leftButtonTimer > 0 && Input.GetButtonDown("Fire1") && !isBlocking) || (rightButtonTimer > 0 && leftButtonTimer > 0 && Input.GetKeyDown(KeyCode.LeftShift) && !isBlocking))
            {
                ShootFireBall(); 
            }

            switch (Input.GetAxis("Vertical"))
            {
                case 0:
                    anim.SetBool("isCrouching", false);
                    break;
                case float i when i > 0 && i <= 1:
                    //PlayerJump();
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
                rightButtonTimer = 2.0f; 
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
                leftButtonTimer = 2.0f; 
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                anim.SetBool("isWalking", false);

            }
            if ((Input.GetButtonDown("Fire1") && !isBlocking) || (Input.GetMouseButtonDown(0) && !isBlocking))
            {
                PunchEnemy(); 
            }
            if (Input.GetButtonUp("Fire1") || Input.GetMouseButtonUp(0))
            {
                anim.SetBool("isPunching", false);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && !isBlocking)
            {  
                PunchEnemy();
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                anim.SetBool("isPunching", false);

            }
            if ((Input.GetButtonDown("Fire2") && !isBlocking) || (!isBlocking && Input.GetMouseButtonDown(1)))
            {
                KickEnemy();
            }
            if (Input.GetButtonUp("Fire2") || Input.GetMouseButtonUp(1))
            {
                anim.SetBool("isKicking", false);
            }

            if (Input.GetKeyDown(KeyCode.LeftControl) && !isBlocking)
            {  
                KickEnemy();
            }
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                anim.SetBool("isKicking", false);
            }

            if (Input.GetButtonDown("Fire3") || Input.GetMouseButtonDown(2))
            {
                anim.SetBool("isBlocking", true);
                isBlocking = true;
            }
            if (Input.GetButtonUp("Fire3") || Input.GetMouseButtonUp(2))
            {
                anim.SetBool("isBlocking", false);
                isBlocking = false;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                anim.SetBool("isBlocking", true);
                isBlocking = true;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                anim.SetBool("isBlocking", false);
                isBlocking = false;
            }
        }
    }

    void FlipX()
    {
        facingRight = !facingRight;
        Quaternion theRotaion = transform.localRotation;
        if(facingRight)
            theRotaion = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        if(!facingRight)
            theRotaion = Quaternion.Euler(new Vector3(180f, 0f, 180f));
        transform.localRotation = theRotaion;
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

    void ShootFireBall()
    {
        audioSource.PlayOneShot(enemyFireballSound, 0.45f);
        GameObject clone = Instantiate(fireball, punchCheck.position, punchCheck.rotation);
        Physics2D.IgnoreCollision(clone.GetComponent<Collider2D>(), rb.GetComponent<Collider2D>());
        Rigidbody2D shot = clone.GetComponent<Rigidbody2D>();
        shot.AddForce(transform.right * 30, ForceMode2D.Impulse);
        Destroy(clone.gameObject, 1f);
        leftButtonTimer = 0;
        rightButtonTimer = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fireball")
        {
            if(!isBlocking)
                playerHealthBar.playerHealthLevel -= 1;
            Destroy(collision.gameObject);
        }
    }
}
