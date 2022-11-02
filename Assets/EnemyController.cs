using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyHealthBar enemyHealthBar; 

    // Start is called before the first frame update
    void Start()
    {
        enemyHealthBar = FindObjectOfType<EnemyHealthBar>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        enemyHealthBar.enemyHealthLevel -= 1; 
    }

}
