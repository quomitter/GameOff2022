using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{

    public int randomNumber;
    public Transform enemyPosition;
    public Transform playerPosition;
    private PlayerHealthBar playerHealthBar;
    private EnemyHealthBar enemyHealthBar;
    public Transform punchCheck;
    public Transform kickCheck;
    public float attackRange;
    public LayerMask whatIsPlayer;
    public Rigidbody2D enemyRB;
    public Animator anim;
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask whatIsGround;

    // Start is called before the first frame update
    void Start()
    {
        playerHealthBar = FindObjectOfType<PlayerHealthBar>();
        enemyHealthBar = FindObjectOfType<EnemyHealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealthBar.enemyHealthLevel <= 0)
            SceneManager.LoadScene(0);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, whatIsGround);
        anim.SetBool("isPunching", false);
        anim.SetBool("isKicking", false);

        randomNumber = Random.Range(1, 50);

        switch (randomNumber)
        {
            case 1:
                //Move towards player
                transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, 0.1f);
                break;
            case 2:
                //Kick player
                KickPlayer(); 
                break;
            case 3:
                //Punch player
                PunchPlayer();
                break;
            case 4:
                //Jump
                if(isGrounded)
                    enemyRB.AddForce(transform.up * 500, ForceMode2D.Force);
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9: 
                break;
            case 10:
                break;  
        }
    }

    void PunchPlayer()
    {
        anim.SetBool("isPunching", true); 
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(punchCheck.position, attackRange, whatIsPlayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            playerHealthBar.playerHealthLevel -= 1;
        }
    }

    void KickPlayer()
    {
        anim.SetBool("isKicking", true); 
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(kickCheck.position, attackRange, whatIsPlayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            playerHealthBar.playerHealthLevel -= 1;
        }
    }


}
