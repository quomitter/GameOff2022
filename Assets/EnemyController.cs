using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    public GameObject fireball;
    public int randomNumber;
    public Transform enemyPosition;
    public Transform playerPosition;
    private PlayerHealthBar playerHealthBar;
    private EnemyHealthBar enemyHealthBar;
    private PlayerController playerController;
    private EnemyBlockBar enemyBlockBar;
    public Transform punchCheck;
    public Transform kickCheck;
    public float attackRange;
    public LayerMask whatIsPlayer;
    public Rigidbody2D enemyRB;
    public Animator anim;
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public bool facingRight = false;
    public float movementSpeed = 1;
    public float speed = 10.0f;
    public float diststop = 0.4f;
    public bool enemyHit;
    public AudioSource audioSource;
    public AudioClip punchSound;
    public AudioClip kickSound;
    public AudioClip enemyFireballSound;
    public int blockCounterLocal = 3;
    public bool isBlocking;
    public float blockTimer;
    public float punchTimer;
    public float kickTimer;
    public float randomTimer;
    public float moveTimer;
    public float fireballTimer;
    public float speedOfTimers;
    public float easyTimer;
    public float hardTimer;
    public int stepCounter;
    public GameObject lightning;
    public Transform playerLightningPoint;
    public bool isInKnockback;
    public float knockbackTimer;
    public string difficulty;
    public Canvas difficultyCanvas;
    public Sprite[] fireballCooldown;
    public Sprite[] lightningCooldown;
    public SpriteRenderer fireballCooldownRenderer;
    public SpriteRenderer lightningCooldownRenderer;
    public float fireballCoolDownTimer;
    public float lightningCoolDownTimer;


    private Vector2 Position
    {
        get
        {
            return transform.position;
        }
        set
        {
            transform.position = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        playerHealthBar = FindObjectOfType<PlayerHealthBar>();
        enemyHealthBar = FindObjectOfType<EnemyHealthBar>();
        playerController = FindObjectOfType<PlayerController>();
        enemyBlockBar = FindObjectOfType<EnemyBlockBar>();
        audioSource = GetComponent<AudioSource>();
        speedOfTimers = 0.2f;
        easyTimer = 0.4f;
        hardTimer = 0.1f;
        blockTimer = speedOfTimers;
        punchTimer = speedOfTimers;
        kickTimer = speedOfTimers;
        randomTimer = speedOfTimers;
        moveTimer = speedOfTimers;
        fireballTimer = speedOfTimers;
        knockbackTimer = 0;
        difficulty = "Normal";
        difficultyCanvas.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.gameIsActive)
        {
            blockTimer -= Time.deltaTime;
            punchTimer -= Time.deltaTime;
            kickTimer -= Time.deltaTime;
            randomTimer -= Time.deltaTime;
            moveTimer -= Time.deltaTime;
            fireballTimer -= Time.deltaTime;
            knockbackTimer -= Time.deltaTime;

            playerController.playerHit = false;
            if (playerPosition.position.x < enemyPosition.position.x)
            {
                transform.localRotation = Quaternion.Euler(new Vector3(180f, 0f, 180f));
                facingRight = false;
            }
            else
            {
                transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                facingRight = true;
            }

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, whatIsGround);

            if (punchTimer < 0)
            {
                anim.SetBool("isPunching", false);

                if (difficulty == "Easy")
                {
                    punchTimer = easyTimer;
                }
                else if (difficulty == "Normal")
                {
                    punchTimer = speedOfTimers;
                }
                else if (difficulty == "Hard")
                {
                    punchTimer = hardTimer;
                }
            }

            if (fireballCoolDownTimer <= 0)
            {
                fireballCooldownRenderer.sprite = fireballCooldown[0];
            }
            else
            {
                fireballCooldownRenderer.sprite = fireballCooldown[1];
            }
            if (lightningCoolDownTimer <= 0)
            {
                lightningCooldownRenderer.sprite = lightningCooldown[0];
            }
            else
            {
                lightningCooldownRenderer.sprite = lightningCooldown[1];
            }

            if (kickTimer < 0)
            {
                anim.SetBool("isKicking", false);
                if (difficulty == "Easy")
                {
                    kickTimer = easyTimer;
                }
                else if (difficulty == "Normal")
                {
                    kickTimer = speedOfTimers;
                }
                else if (difficulty == "Hard")
                {
                    kickTimer = hardTimer;
                }
            }


            if (blockTimer < 0)
            {
                anim.SetBool("isBlocking", false);
                isBlocking = false;
                if (difficulty == "Easy")
                {
                    blockTimer = easyTimer;
                }
                else if (difficulty == "Normal")
                {
                    blockTimer = speedOfTimers;
                }
                else if (difficulty == "Hard")
                {
                    blockTimer = hardTimer;
                }
            }

            if (randomTimer < 0)
            {
                randomNumber = Random.Range(1, 10);

                if (difficulty == "Easy")
                {
                    randomTimer = easyTimer;
                }
                else if (difficulty == "Normal")
                {
                    randomTimer = speedOfTimers;
                }
                else if (difficulty == "Hard")
                {
                    randomTimer = hardTimer;
                }
            }
            if (knockbackTimer < 0)
            {
                isInKnockback = false;
                anim.SetBool("isDazed", false);
            }


            float dist = Vector2.Distance(Position, playerPosition.position);
            if (moveTimer < 0)
            {

                float step = speed * Time.deltaTime;
                if (dist >= diststop && !isInKnockback)
                {
                    Position = Vector2.MoveTowards(Position, playerPosition.position, step);
                    enemyRB.MovePosition(Position);
                    anim.SetBool("isWalking", true);
                }
                stepCounter++;
                if (stepCounter > 1)
                {
                    moveTimer = speedOfTimers;
                    stepCounter = 0;
                    anim.SetBool("isWalking", false);
                }

            }
            if (dist < 2.0f)
            {
                switch (randomNumber)
                {
                    case 1:
                        //PunchPlayer
                        if (!isBlocking || !isInKnockback)
                            PunchPlayer();
                        randomNumber = 0;
                        break;
                    case 2:
                        //Kick player
                        if (!isBlocking || !isInKnockback)
                            KickPlayer();
                        randomNumber = 0;
                        break;
                    case 3:
                        //Punch player
                        if (!isBlocking || !isInKnockback)
                            PunchPlayer();
                        randomNumber = 0;
                        break;
                    case 4:
                        //Jump
                        if (isGrounded)
                            enemyRB.AddForce(transform.up * 500, ForceMode2D.Force);
                        randomNumber = 0;
                        break;
                    case 5:
                        if (!isInKnockback)
                            BlockPlayer();
                        randomNumber = 0;
                        break;
                    case 9:
                        if (!isInKnockback)
                            BlowBack();
                        randomNumber = 0;
                        break;
                }
            }
            if (dist > 3.0f)
            {
                switch (randomNumber)
                {
                    case 6:
                        if (fireballTimer < 0)
                        {
                            if (!isInKnockback)
                                ShootFireBall();
                            fireballTimer = speedOfTimers;
                            randomNumber = 0;
                        }

                        break;
                    case 7:
                        if (isInKnockback)
                            BlockPlayer();
                        randomNumber = 0;
                        break;
                    case 8:
                        if (!isInKnockback)
                            RainLightning();
                        randomNumber = 0;
                        break;


                }
            }
        }
    }

    void PunchPlayer()
    {
        anim.SetBool("isPunching", true);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(punchCheck.position, attackRange, whatIsPlayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            playerController.playerHit = true;
            if (!playerController.isBlocking)
                playerHealthBar.playerHealthLevel -= 1;
            audioSource.PlayOneShot(punchSound);
        }


    }

    void KickPlayer()
    {
        anim.SetBool("isKicking", true);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(kickCheck.position, attackRange, whatIsPlayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            playerController.playerHit = true;
            if (!playerController.isBlocking)
                playerHealthBar.playerHealthLevel -= 1;
            audioSource.PlayOneShot(kickSound);
        }

    }

    void BlockPlayer()
    {
        if (enemyBlockBar.canBlock && (enemyBlockBar.blockCounter == 1 || enemyBlockBar.blockCounter == 2 || enemyBlockBar.blockCounter == 3))
        {
            anim.SetBool("isBlocking", true);
            isBlocking = true;
        }

    }

    void ShootFireBall()
    {
        if (fireballCoolDownTimer <= 0)
        {
            audioSource.PlayOneShot(enemyFireballSound, 0.45f);
            GameObject clone = Instantiate(fireball, punchCheck.position, punchCheck.rotation);
            Physics2D.IgnoreCollision(clone.GetComponent<Collider2D>(), enemyRB.GetComponent<Collider2D>());
            Rigidbody2D shot = clone.GetComponent<Rigidbody2D>();
            shot.AddForce(transform.right * 30, ForceMode2D.Impulse);
            Destroy(clone.gameObject, 1f);
            randomNumber = 0;
            fireballCoolDownTimer = 1f;
        }

    }
    void RainLightning()
    {
        if (lightningCoolDownTimer <= 0)
        {
            audioSource.PlayOneShot(enemyFireballSound, 0.45f);
            GameObject clone = Instantiate(lightning, playerLightningPoint.position, playerLightningPoint.rotation);
            Physics2D.IgnoreCollision(clone.GetComponent<Collider2D>(), enemyRB.GetComponent<Collider2D>());
            Rigidbody2D shot = clone.GetComponent<Rigidbody2D>();
            Destroy(clone.gameObject, 0.4f);
            if (!playerController.isBlocking)
                playerHealthBar.playerHealthLevel -= 1;
            lightningCoolDownTimer = 1f;
        }

    }

    void BlowBack()
    {

        if (playerController.facingRight)
        {

            playerController.rb.AddForce(new Vector2(-400, 250));
            playerController.isInKnockback = true;
            playerController.knockbackTimer = 1f;
            playerController.anim.SetBool("isDazed", true);


        }
        if (!playerController.facingRight)
        {

            playerController.rb.AddForce(new Vector2(400, 250));
            playerController.isInKnockback = true;
            playerController.knockbackTimer = 1f;
            playerController.anim.SetBool("isDazed", true);

        }

    }

    public void SetEasy()
    {
        difficulty = "Easy";
        playerController.gameIsActive = true;
        difficultyCanvas.enabled = false;
    }
    public void SetNormal()
    {
        difficulty = "Normal";
        playerController.gameIsActive = true;
        difficultyCanvas.enabled = false;
    }
    public void SetHard()
    {
        difficulty = "Hard";
        playerController.gameIsActive = true;
        difficultyCanvas.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fireball")
        {
            if (!isBlocking)
                enemyHealthBar.enemyHealthLevel -= 1;
            Destroy(collision.gameObject);
        }
    }

}
