using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;
using System.Collections.Concurrent;

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
    public bool isBlocking;
    public float blockTimer;
    public float punchTimer;
    public float kickTimer;
    public float randomTimer;
    public float moveTimer;
    public float fireballTimer; 
    public float speedOfTimers;
    public int stepCounter; 

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
        blockTimer = speedOfTimers;
        punchTimer = speedOfTimers;
        kickTimer = speedOfTimers;
        randomTimer = speedOfTimers;
        moveTimer = speedOfTimers;
        fireballTimer = speedOfTimers;

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
            playerController.playerHit = false;
            if (playerPosition.position.x < enemyPosition.position.x)
            {
                transform.localRotation = Quaternion.Euler(new Vector3(180f, 0f, 180f));
                facingRight = false; 
            } else 
            {
                transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                facingRight = true; 
            }

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, whatIsGround);

            if (punchTimer < 0)
            {
                anim.SetBool("isPunching", false);
                punchTimer = speedOfTimers;
            }


            if (kickTimer < 0)
            {
                anim.SetBool("isKicking", false);
                kickTimer = speedOfTimers;
            }


            if (blockTimer < 0)
            {
                anim.SetBool("isBlocking", false);
                isBlocking = false;                
                blockTimer = speedOfTimers;


            }

            if (randomTimer < 0)
            {
                randomNumber = Random.Range(1, 8);
                randomTimer = speedOfTimers;
            }


            float dist = Vector2.Distance(Position, playerPosition.position);
            if (moveTimer < 0)
            {

                float step = speed * Time.deltaTime;
                if (dist >= diststop)
                {
                    Position = Vector2.MoveTowards(Position, playerPosition.position, step);
                    enemyRB.MovePosition(Position);
                    anim.SetBool("isWalking", true);
                }
                stepCounter++;
                if(stepCounter > 1)
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
                        if (!isBlocking)
                            PunchPlayer();
                        randomNumber = 0;
                        break;
                    case 2:
                        //Kick player
                        if (!isBlocking)
                            KickPlayer();
                        randomNumber = 0;
                        break;
                    case 3:
                        //Punch player
                        if (!isBlocking)
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
                        //BlockPlayer
                        BlockPlayer();
                        enemyBlockBar.blockCounter--;
                        randomNumber = 0;
                        break;
                }
            }if(dist > 3.0f)
            {
                switch (randomNumber)
                {
                    case 6:
                        if(fireballTimer < 0)
                        {
                            ShootFireBall();
                            fireballTimer = speedOfTimers; 
                            randomNumber = 0;
                        }
                            
                        break;
                    case 7:
                        //BlockPlayer
                        BlockPlayer();
                        enemyBlockBar.blockCounter--;
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
        audioSource.PlayOneShot(enemyFireballSound, 0.45f);
        GameObject clone = Instantiate(fireball, punchCheck.position, punchCheck.rotation);
        Physics2D.IgnoreCollision(clone.GetComponent<Collider2D>(), enemyRB.GetComponent<Collider2D>());
        Rigidbody2D shot = clone.GetComponent<Rigidbody2D>();
        shot.AddForce(transform.right * 30, ForceMode2D.Impulse);
        Destroy(clone.gameObject, 1f);
        randomNumber = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Fireball")
        {
            if(!isBlocking)
                enemyHealthBar.enemyHealthLevel -= 1;
            Destroy(collision.gameObject);
        }
    }

}
