using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public SpriteRenderer enemySpriteRenderer; 
    public Sprite[] enemyHealthBar; 
    public int enemyHealthLevel; 

    // Start is called before the first frame update
    void Start()
    {
        enemySpriteRenderer.sprite = enemyHealthBar[10];
    }

    // Update is called once per frame
    void Update()
    {
        switch(enemyHealthLevel){
            case 0:
            enemySpriteRenderer.sprite = enemyHealthBar[0];
            break;
            case 1:
            enemySpriteRenderer.sprite = enemyHealthBar[1];
            break;
            case 2:
            enemySpriteRenderer.sprite = enemyHealthBar[2];
            break; 
            case 3:
            enemySpriteRenderer.sprite = enemyHealthBar[3];
            break; 
            case 4:
            enemySpriteRenderer.sprite = enemyHealthBar[4];
            break;
            case 5:
            enemySpriteRenderer.sprite = enemyHealthBar[5];
            break;
            case 6:
            enemySpriteRenderer.sprite = enemyHealthBar[6];
            break;
            case 7:
            enemySpriteRenderer.sprite = enemyHealthBar[7];
            break; 
            case 8:
            enemySpriteRenderer.sprite = enemyHealthBar[8];
            break;
            case 9:
            enemySpriteRenderer.sprite = enemyHealthBar[9];
            break;
            case 10:
            enemySpriteRenderer.sprite = enemyHealthBar[10];
            break; 
        }
    }
}
