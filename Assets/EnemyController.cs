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

    // Start is called before the first frame update
    void Start()
    {
        playerHealthBar = FindObjectOfType<PlayerHealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        randomNumber = Random.Range(1, 4);

        switch (randomNumber)
        {
            case 1:
                //Move towards player
                Vector2.MoveTowards(enemyPosition.position, playerPosition.position, 0.5f);
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
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(punchCheck.position, attackRange, whatIsPlayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            playerHealthBar.playerHealthLevel -= 1;
        }
    }

    void KickPlayer()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(kickCheck.position, attackRange, whatIsEnemy);
        foreach (Collider2D enemy in hitEnemies)
        {
            playerHealthBar.playerHealthLevel -= 1;
        }
    }


}
