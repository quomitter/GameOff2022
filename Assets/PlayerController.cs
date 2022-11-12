using System.Collections;
using System.Collections.Generic;
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
    private PlayerBlockBar playerBlockBar;
    public bool playerHit;
    public AudioSource audioSource;
    public AudioClip punchSound;
    public AudioClip kickSound;
    public AudioClip enemyFireballSound;
    public bool isBlocking;
    public bool isPunching;
    public bool isKicking;
    public bool isShooting;
    public Canvas winOrLoseCanvas;
    public TMP_Text winOrLose;
    public Button tryAgain;
    public bool gameIsActive;
    public float leftButtonTimer;
    public float rightButtonTimer;
    public GameObject lightning;
    public Transform enemyLightningPoint;
    public float upTimer;
    public float downTimer;
    public float lightningTimer;
    public bool isInKnockback;
    public float knockbackTimer;

    // Start is called before the first frame update
    void Start()
    {
        playerBlockBar = FindObjectOfType<PlayerBlockBar>();
        enemyHealthBar = FindObjectOfType<EnemyHealthBar>();
        playerHealthBar = FindObjectOfType<PlayerHealthBar>();
        enemyController = FindObjectOfType<EnemyController>();
        audioSource = GetComponent<AudioSource>();
        winOrLoseCanvas.enabled = false;
        gameIsActive = true;
        leftButtonTimer = 0;
        rightButtonTimer = 0;
        upTimer = 0;
        downTimer = 0;
        knockbackTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        leftButtonTimer -= Time.deltaTime;
        rightButtonTimer -= Time.deltaTime;
        upTimer -= Time.deltaTime;
        downTimer -= Time.deltaTime;
        knockbackTimer -= Time.deltaTime;

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
            if (knockbackTimer < 0)
            {
                isInKnockback = false;
            }

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, whatIsGround);
            if (Input.GetButtonDown("Jump") && (isGrounded))
            {
                PlayerJump();
            }
            if ((Input.GetKeyDown(KeyCode.W) && isGrounded))
            {
                PlayerJump();
                upTimer = 2.0f;
            }
            if (Input.GetKeyUp(KeyCode.W))
            {

            }
            if (!isInKnockback)
            {
                switch (Input.GetAxis("Horizontal"))
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
            }

            if ((rightButtonTimer > 0 && leftButtonTimer > 0 && Input.GetButtonDown("Fire1") && !isBlocking) || (rightButtonTimer > 0 && leftButtonTimer > 0 && Input.GetKeyDown(KeyCode.LeftShift) && !isBlocking))
            {
                ShootFireBall();
                isShooting = true;
                isShooting = false;
            }
            if ((upTimer > 0 && downTimer > 0 && Input.GetButtonDown("Fire1") && !isBlocking) || (upTimer > 0 && downTimer > 0 && Input.GetKeyDown(KeyCode.LeftShift) && !isBlocking))
            {
                RainLightning();
            }

            if (!isInKnockback)
            {
                switch (Input.GetAxis("Vertical"))
                {
                    case 0:
                        anim.SetBool("isCrouching", false);
                        break;
                    case float i when i == 1:
                        //PlayerJump();
                        upTimer = 2.0f;
                        break;
                    case float i when i == -1:
                        anim.SetBool("isCrouching", true);
                        downTimer = 2.0f;
                        break;

                }
            }


            if (Input.GetKeyDown(KeyCode.S))
            {
                anim.SetBool("isCrouching", true);
                downTimer = 2.0f;
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                anim.SetBool("isCrouching", false);
            }
            if (Input.GetKey(KeyCode.D) && !isInKnockback)
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
            if (Input.GetKey(KeyCode.A) && !isInKnockback)
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
            if ((Input.GetButtonDown("Fire1") && !isKicking && !isShooting && !isBlocking) || (Input.GetMouseButtonDown(0) && !isKicking && !isShooting && !isBlocking))
            {
                PunchEnemy();
                isPunching = true;
            }
            if (Input.GetButtonUp("Fire1") || Input.GetMouseButtonUp(0))
            {
                anim.SetBool("isPunching", false);
                isPunching = false;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && !isBlocking && !isShooting && !isKicking)
            {
                PunchEnemy();
                isPunching = true;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                anim.SetBool("isPunching", false);
                isPunching = false;

            }
            if ((Input.GetButtonDown("Fire2") && !isBlocking && !isShooting && !isPunching) || (!isBlocking && !isShooting && !isPunching && Input.GetMouseButtonDown(1)))
            {
                KickEnemy();
                isKicking = true;
            }
            if (Input.GetButtonUp("Fire2") || Input.GetMouseButtonUp(1))
            {
                anim.SetBool("isKicking", false);
                isKicking = false;
            }

            if (Input.GetKeyDown(KeyCode.LeftControl) && !isBlocking && !isShooting && !isPunching)
            {
                KickEnemy();
                isKicking = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                anim.SetBool("isKicking", false);
                isKicking = false;
            }

            if ((Input.GetButtonDown("Fire3") && playerBlockBar.canBlock && !isBlocking && !isShooting && !isPunching) || (Input.GetMouseButtonDown(2) && playerBlockBar.canBlock && !isBlocking && !isShooting && !isPunching) || (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.LeftShift) && !isBlocking))
            {
                anim.SetBool("isBlocking", true);
                isBlocking = true;
            }
            if (Input.GetButtonUp("Fire3") || Input.GetMouseButtonUp(2))
            {
                anim.SetBool("isBlocking", false);
                isBlocking = false;
                if (playerBlockBar.blockCounter >= 0)
                    playerBlockBar.blockCounter--;
            }
            if (Input.GetKey(KeyCode.Space) && playerBlockBar.canBlock && !isBlocking && !isShooting && !isPunching)
            {
                anim.SetBool("isBlocking", true);
                isBlocking = true;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                anim.SetBool("isBlocking", false);
                isBlocking = false;
                if (playerBlockBar.blockCounter >= 0)
                    playerBlockBar.blockCounter--;
            }
            if ((Input.GetMouseButtonDown(0) && Input.GetMouseButtonDown(1) && !isBlocking) || (Input.GetButtonDown("Fire1") && Input.GetButtonDown("Fire2") && !isBlocking))
            {
                BlowBack();
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.LeftControl) && !isBlocking)
            {
                BlowBack();
            }

        }
    }

    void FlipX()
    {
        facingRight = !facingRight;
        Quaternion theRotaion = transform.localRotation;
        if (facingRight)
            theRotaion = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        if (!facingRight)
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
        if (isGrounded)
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

    void RainLightning()
    {
        audioSource.PlayOneShot(enemyFireballSound, 0.45f);
        GameObject clone = Instantiate(lightning, enemyLightningPoint.position, enemyLightningPoint.rotation);
        Physics2D.IgnoreCollision(clone.GetComponent<Collider2D>(), rb.GetComponent<Collider2D>());
        Rigidbody2D shot = clone.GetComponent<Rigidbody2D>();
        Destroy(clone.gameObject, 0.4f);
        upTimer = 0;
        downTimer = 0;
        if (!enemyController.isBlocking)
            enemyHealthBar.enemyHealthLevel -= 1;
    }

    void BlowBack()
    {

        if (enemyController.facingRight)
        {
            enemyController.enemyRB.AddForce(new Vector2(-400, 250));
            enemyController.isInKnockback = true;
            enemyController.knockbackTimer = 1f;
        }
        if (!enemyController.facingRight)
        {
            enemyController.enemyRB.AddForce(new Vector2(400, 250));
            enemyController.isInKnockback = true;
            enemyController.knockbackTimer = 1f;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fireball")
        {
            if (!isBlocking)
                playerHealthBar.playerHealthLevel -= 1;
            Destroy(collision.gameObject);
        }
    }
}
