using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{

    public int randomNumber;
    public Transform enemyPosition;
    public Transform playerPosition;
    private PlayerHealthBar playerHealthBar;
    private EnemyHealthBar enemyHealthBar;
    private PlayerController playerController;
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
    public float diststop = 0.2f;
    public bool enemyHit;
    public AudioSource audioSource;
    public AudioClip punchSound;
    public AudioClip kickSound;
    public bool isBlocking;
    public float blockTimer;
    public float punchTimer;
    public float kickTimer;
    public float randomTimer;
    public float speedOfTimers; 

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
        audioSource = GetComponent<AudioSource>();
        speedOfTimers = 0.2f; 
        blockTimer = speedOfTimers;
        punchTimer = speedOfTimers;
        kickTimer = speedOfTimers;
        randomTimer = speedOfTimers; 

    }

    // Update is called once per frame
    void Update()
    {
        blockTimer -= Time.deltaTime;
        punchTimer -= Time.deltaTime;
        kickTimer -= Time.deltaTime;
        randomTimer -= Time.deltaTime;
        playerController.playerHit = false;
        if (playerPosition.position.x < enemyPosition.position.x)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else { transform.localScale = new Vector3(-1f, 1f, 1f); }
        if (enemyHealthBar.enemyHealthLevel <= 0)
            SceneManager.LoadScene(0);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, whatIsGround);

        if(punchTimer < 0)
        {
            anim.SetBool("isPunching", false);
            punchTimer = speedOfTimers; 
        }
        

        if(kickTimer < 0)
        {
            anim.SetBool("isKicking", false);
            kickTimer = speedOfTimers;
        }
        
        
        if(blockTimer < 0)
        {
            anim.SetBool("isBlocking", false);
            isBlocking = false;
            blockTimer = speedOfTimers; 
        }
        
        if(randomTimer < 0)
        {
            randomNumber = Random.Range(1, 6);
            randomTimer = speedOfTimers; 
        }
        

        switch (randomNumber)
        {
            case 1:
                float dist = Vector2.Distance(Position, playerPosition.position);
                float step = speed * Time.deltaTime;
                if (dist >= diststop)
                Position = Vector2.MoveTowards(Position, playerPosition.position, step);
                enemyRB.MovePosition(Position);
                randomNumber = 0; 
                break;
            case 2:
                //Kick player
                if(!isBlocking)
                     KickPlayer();
                randomNumber = 0;
                break;
            case 3:
                //Punch player
                if(!isBlocking)
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
                randomNumber = 0;
                break;
        }
    }

    void PunchPlayer()
    {
        if (!playerController.isBlocking)
        {
            anim.SetBool("isPunching", true);
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(punchCheck.position, attackRange, whatIsPlayer);
            foreach (Collider2D enemy in hitEnemies)
            {
                playerController.playerHit = true;
                playerHealthBar.playerHealthLevel -= 1;
                audioSource.PlayOneShot(punchSound);
            }
        }

    }

    void KickPlayer()
    {
        if (!playerController.isBlocking)
        {
            anim.SetBool("isKicking", true);
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(kickCheck.position, attackRange, whatIsPlayer);
            foreach (Collider2D enemy in hitEnemies)
            {
                playerController.playerHit = true;
                playerHealthBar.playerHealthLevel -= 1;
                audioSource.PlayOneShot(kickSound);
            }
        }
    }

    void BlockPlayer()
    {
        anim.SetBool("isBlocking", true);
        isBlocking = true;
    }

    void FlipX()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
