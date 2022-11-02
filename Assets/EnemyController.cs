using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public int randomNumber;
    public Transform enemyPosition;
    public Transform playerPosition;
    private PlayerHealthBar playerHealthBar;
    public Transform punchCheck;
    public Transform kickCheck;
    public float attackRange;
    public LayerMask whatIsPlayer;
    public Rigidbody2D enemyRB;
    public Animator anim; 

    // Start is called before the first frame update
    void Start()
    {
        playerHealthBar = FindObjectOfType<PlayerHealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isPunching", false);
        anim.SetBool("isKicking", false);

        randomNumber = Random.Range(1, 4);

        switch (randomNumber)
        {
            case 1:
                //Move towards player
                transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, 0.5f);
                break;
            case 2:
                //Kick player
                KickPlayer(); 
                break;
            case 3:
                //Punch player
                PunchPlayer();
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
