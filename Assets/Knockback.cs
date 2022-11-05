using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{

    public Rigidbody2D playerRigidbody2D;
    public Rigidbody2D enemyRigidbody2D;
    private PlayerController playerController;
    private EnemyController enemyController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        enemyController = FindObjectOfType<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.gameIsActive)
        {
            if (playerController.playerHit && enemyController.facingRight && gameObject.tag == "Player")
            {
                playerRigidbody2D.AddForce(new Vector2(-200, 50));
            }
            if (playerController.playerHit && !enemyController.facingRight && gameObject.tag == "Player")
                playerRigidbody2D.AddForce(new Vector2(200, 50));
            if (enemyController.enemyHit && playerController.facingRight && gameObject.tag == "Enemy")
            {
                enemyRigidbody2D.AddForce(new Vector2(200, 50));
            }

            if (enemyController.enemyHit && !playerController.facingRight && gameObject.tag == "Enemy")
                enemyRigidbody2D.AddForce(new Vector2(-200, 50));
        }
    }
}
