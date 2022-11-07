using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlockBar : MonoBehaviour
{
    private EnemyController enemyController;
    public SpriteRenderer enemyBlockBarSpriteRenderer1, enemyBlockBarSpriteRenderer2, enemyBlockBarSpriteRenderer3;
    public Sprite[] enemyBlockBar;
    public int enemyBlockLevelOne;
    public int enemyBlockLevelTwo;
    public int enemyBlockLevelThree;
    public float enemyBlockRechargeRate;
    public bool blockOneFull, blockTwoFull, blockThreeFull;
    public bool canBlock;
    public int blockCounter;
    public bool SwitchCounted;

    // Start is called before the first frame update
    void Start()
    {
        enemyController = FindObjectOfType<EnemyController>();
        enemyBlockLevelOne = 10;
        enemyBlockLevelTwo = 10;
        enemyBlockLevelThree = 10;
        blockOneFull = true;
        blockTwoFull = true;
        blockThreeFull = true;
        enemyBlockRechargeRate = 0.1f;
        blockCounter = 3;
        SwitchCounted = false; 
    }

    // Update is called once per frame
    void Update()
    {
        enemyBlockRechargeRate -= Time.deltaTime;

        if (enemyBlockRechargeRate <= 0)
        {
            if (!blockOneFull)
            {
                if (enemyBlockLevelOne <= 9)
                    enemyBlockLevelOne++;
                enemyBlockRechargeRate = 0.1f;
                if (enemyBlockLevelOne == 10)
                {
                    blockOneFull = true;
                    canBlock = true;
                    blockCounter = 1;
                }

            }
            else if (!blockTwoFull)
            {
                if (enemyBlockLevelTwo <= 9)
                    enemyBlockLevelTwo++;
                enemyBlockRechargeRate = 0.1f;
                if (enemyBlockLevelTwo == 10)
                {
                    blockTwoFull = true;
                    canBlock = true;
                    blockCounter = 2;
                }

            }
            else if (!blockThreeFull)
            {
                if (enemyBlockLevelThree <= 9)
                    enemyBlockLevelThree++;
                enemyBlockRechargeRate = 0.1f;
                if (enemyBlockLevelThree == 10)
                {
                    blockThreeFull = true;
                    canBlock = true;
                    blockCounter = 3;
                }

            }
        }

        if (blockThreeFull)
        {
            blockCounter = 3;
            canBlock = true;
        }  
        else if (blockTwoFull)
        {
            blockCounter = 2;
            canBlock = true;
        }
        else if (blockOneFull)
        {
            blockCounter = 1;
            canBlock = true;
        }

        if (enemyController.isBlocking)
        {
            if (blockOneFull && blockTwoFull && blockThreeFull && blockCounter == 3)
            {
                enemyBlockLevelOne = 10;
                enemyBlockLevelTwo = 10;
                enemyBlockLevelThree = 0;
                canBlock = true;
            }
            else if (blockOneFull && blockTwoFull && !blockThreeFull && blockCounter == 2)
            {
                enemyBlockLevelOne = 10;
                enemyBlockLevelThree = 0;
                enemyBlockLevelTwo = 0;
                canBlock = true;
            }
            else if (blockOneFull && !blockTwoFull && !blockThreeFull && blockCounter == 1)
            {
                enemyBlockLevelThree = 0;
                enemyBlockLevelTwo = 0;
                enemyBlockLevelOne = 0;
                canBlock = true;
            }
            else if (!blockOneFull && !blockTwoFull && !blockThreeFull && blockCounter <= 0)
            {
                canBlock = false;
                blockCounter = 0;
            }
        }

        switch (enemyBlockLevelOne)
        {
            case 0:
                enemyBlockBarSpriteRenderer1.sprite = enemyBlockBar[10];
                blockOneFull = false;
                break;
            case 1:
                enemyBlockBarSpriteRenderer1.sprite = enemyBlockBar[9];
                blockOneFull = false;
                break;
            case 2:
                enemyBlockBarSpriteRenderer1.sprite = enemyBlockBar[8];
                blockOneFull = false;
                break;
            case 3:
                enemyBlockBarSpriteRenderer1.sprite = enemyBlockBar[7];
                blockOneFull = false;
                break;
            case 4:
                enemyBlockBarSpriteRenderer1.sprite = enemyBlockBar[6];
                blockOneFull = false;
                break;
            case 5:
                enemyBlockBarSpriteRenderer1.sprite = enemyBlockBar[5];
                blockOneFull = false;
                break;
            case 6:
                enemyBlockBarSpriteRenderer1.sprite = enemyBlockBar[4];
                blockOneFull = false;
                break;
            case 7:
                enemyBlockBarSpriteRenderer1.sprite = enemyBlockBar[3];
                blockOneFull = false;
                break;
            case 8:
                enemyBlockBarSpriteRenderer1.sprite = enemyBlockBar[2];
                blockOneFull = false;
                break;
            case 9:
                enemyBlockBarSpriteRenderer1.sprite = enemyBlockBar[1];
                blockOneFull = false;
                break;
            case 10:
                enemyBlockBarSpriteRenderer1.sprite = enemyBlockBar[0];
                blockOneFull = true;
                canBlock = true;
                break;
        }
        switch (enemyBlockLevelTwo)
        {
            case 0:
                enemyBlockBarSpriteRenderer2.sprite = enemyBlockBar[10];
                blockTwoFull = false;
                break;
            case 1:
                enemyBlockBarSpriteRenderer2.sprite = enemyBlockBar[9];
                blockTwoFull = false;
                break;
            case 2:
                enemyBlockBarSpriteRenderer2.sprite = enemyBlockBar[8];
                blockTwoFull = false;
                break;
            case 3:
                enemyBlockBarSpriteRenderer2.sprite = enemyBlockBar[7];
                blockTwoFull = false;
                break;
            case 4:
                enemyBlockBarSpriteRenderer2.sprite = enemyBlockBar[6];
                blockTwoFull = false;
                break;
            case 5:
                enemyBlockBarSpriteRenderer2.sprite = enemyBlockBar[5];
                blockTwoFull = false;
                break;
            case 6:
                enemyBlockBarSpriteRenderer2.sprite = enemyBlockBar[4];
                blockTwoFull = false;
                break;
            case 7:
                enemyBlockBarSpriteRenderer2.sprite = enemyBlockBar[3];
                blockTwoFull = false;
                break;
            case 8:
                enemyBlockBarSpriteRenderer2.sprite = enemyBlockBar[2];
                blockTwoFull = false;
                break;
            case 9:
                enemyBlockBarSpriteRenderer2.sprite = enemyBlockBar[1];
                blockTwoFull = false;
                break;
            case 10:
                enemyBlockBarSpriteRenderer2.sprite = enemyBlockBar[0];
                blockTwoFull = true;
                canBlock = true;
                break;
        }
        switch (enemyBlockLevelThree)
        {
            case 0:
                enemyBlockBarSpriteRenderer3.sprite = enemyBlockBar[10];
                blockThreeFull = false;
                break;
            case 1:
                enemyBlockBarSpriteRenderer3.sprite = enemyBlockBar[9];
                blockThreeFull = false;
                break;
            case 2:
                enemyBlockBarSpriteRenderer3.sprite = enemyBlockBar[8];
                blockThreeFull = false;
                break;
            case 3:
                enemyBlockBarSpriteRenderer3.sprite = enemyBlockBar[7];
                blockThreeFull = false;
                break;
            case 4:
                enemyBlockBarSpriteRenderer3.sprite = enemyBlockBar[6];
                blockThreeFull = false;
                break;
            case 5:
                enemyBlockBarSpriteRenderer3.sprite = enemyBlockBar[5];
                blockThreeFull = false;
                break;
            case 6:
                enemyBlockBarSpriteRenderer3.sprite = enemyBlockBar[4];
                blockThreeFull = false;
                break;
            case 7:
                enemyBlockBarSpriteRenderer3.sprite = enemyBlockBar[3];
                blockThreeFull = false;
                break;
            case 8:
                enemyBlockBarSpriteRenderer3.sprite = enemyBlockBar[2];
                blockThreeFull = false;
                break;
            case 9:
                enemyBlockBarSpriteRenderer3.sprite = enemyBlockBar[1];
                blockThreeFull = false;
                break;
            case 10:
                enemyBlockBarSpriteRenderer3.sprite = enemyBlockBar[0];
                blockThreeFull = true;
                canBlock = true;
                break;
        }
    }
}
