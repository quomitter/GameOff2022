using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // Start is called before the first frame update
    void Start()
    {
        enemyHealthBar = FindObjectOfType<EnemyHealthBar>();
        playerHealthBar = FindObjectOfType<PlayerHealthBar>();
        enemyController = FindObjectOfType<EnemyController>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyController.enemyHit = false;
        if (playerHealthBar.playerHealthLevel <= 0)
            SceneManager.LoadScene(0);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, whatIsGround);

        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Force);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {

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
        if (Input.GetKeyDown(KeyCode.Space) && !isBlocking)
        {
            anim.SetBool("isPunching", true);
            PunchEnemy();

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("isPunching", false);

        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isBlocking)
        {
            anim.SetBool("isKicking", true);
            KickEnemy();

        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.SetBool("isKicking", false);
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

    void FlipX()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void PunchEnemy()
    {
        if (!enemyController.isBlocking)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(punchCheck.position, attackRange, whatIsEnemy);
            foreach (Collider2D enemy in hitEnemies)
            {
                enemyController.enemyHit = true;
                enemyHealthBar.enemyHealthLevel -= 1;
                audioSource.PlayOneShot(punchSound);
            }
        }
    }

    void KickEnemy()
    {
        if (!enemyController.isBlocking)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(kickCheck.position, attackRange, whatIsEnemy);
            foreach (Collider2D enemy in hitEnemies)
            {
                enemyController.enemyHit = true;
                enemyHealthBar.enemyHealthLevel -= 1;
                audioSource.PlayOneShot(kickSound);
            }
        }
    }

}
