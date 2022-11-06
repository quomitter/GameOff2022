using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockBar : MonoBehaviour
{
    private PlayerController playerController;
    public SpriteRenderer playerBlockBarSpriteRenderer1, playerBlockBarSpriteRenderer2, playerBlockBarSpriteRenderer3;
    public Sprite[] playerBlockBar;
    public int playerBlockLevelOne;
    public int playerBlockLevelTwo;
    public int playerBlockLevelThree;
    public float playerBlockRechargeRate;
    public bool blockOneFull, blockTwoFull, blockThreeFull;
    public bool canBlock;
    public int blockCounter;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerBlockLevelOne = 10;
        playerBlockLevelTwo = 10;
        playerBlockLevelThree = 10;
        blockOneFull = true;
        blockTwoFull = true;
        blockThreeFull = true;
        playerBlockRechargeRate = 0.1f;
        blockCounter = 3; 
    }

    // Update is called once per frame
    void Update()
    {
        playerBlockRechargeRate -= Time.deltaTime;

        if (playerBlockRechargeRate <= 0)
        {
            if (!blockOneFull)
            {
                if (playerBlockLevelOne <= 9)
                    playerBlockLevelOne++;
                playerBlockRechargeRate = 0.1f;
                if (playerBlockLevelOne == 10)
                {
                    blockOneFull = true;
                    canBlock = true;
                    blockCounter = 1; 
                }
                    
            }
            else if (!blockTwoFull)
            {
                if (playerBlockLevelTwo <= 9)
                    playerBlockLevelTwo++;
                playerBlockRechargeRate = 0.1f;
                if (playerBlockLevelTwo == 10)
                {
                    blockTwoFull = true;
                    canBlock = true;
                    blockCounter = 2; 
                }
                    
            }
            else if (!blockThreeFull)
            {
                if (playerBlockLevelThree <= 9)
                    playerBlockLevelThree++;
                playerBlockRechargeRate = 0.1f;
                if (playerBlockLevelThree == 10)
                {
                    blockThreeFull = true;
                    canBlock = true;
                    blockCounter = 3; 
                }
                    
            }
        }

        if (playerController.isBlocking)
        {
            if (blockOneFull && blockTwoFull && blockThreeFull && blockCounter == 3)
            {
                playerBlockLevelOne = 10; 
                playerBlockLevelTwo = 10;
                playerBlockLevelThree = 0;
                canBlock = true;
            }
            else if (blockOneFull && blockTwoFull && !blockThreeFull && blockCounter == 2)
            {
                playerBlockLevelOne = 10; 
                playerBlockLevelThree = 0; 
                playerBlockLevelTwo = 0;
                canBlock = true;
            }
            else if (blockOneFull && !blockTwoFull && !blockThreeFull && blockCounter == 1)
            {
                playerBlockLevelThree = 0;
                playerBlockLevelTwo = 0;
                playerBlockLevelOne = 0;
                canBlock = true;
            }
            else if (!blockOneFull && !blockTwoFull && !blockThreeFull)
            {
                canBlock = false;
                blockCounter = 0; 
            }
        }

        switch (playerBlockLevelOne)
        {
            case 0:
                playerBlockBarSpriteRenderer1.sprite = playerBlockBar[10];
                blockOneFull = false;
                break;
            case 1:
                playerBlockBarSpriteRenderer1.sprite = playerBlockBar[9];
                blockOneFull = false;
                break;
            case 2:
                playerBlockBarSpriteRenderer1.sprite = playerBlockBar[8];
                blockOneFull = false;
                break;
            case 3:
                playerBlockBarSpriteRenderer1.sprite = playerBlockBar[7];
                blockOneFull = false;
                break;
            case 4:
                playerBlockBarSpriteRenderer1.sprite = playerBlockBar[6];
                blockOneFull = false;
                break;
            case 5:
                playerBlockBarSpriteRenderer1.sprite = playerBlockBar[5];
                blockOneFull = false;
                break;
            case 6:
                playerBlockBarSpriteRenderer1.sprite = playerBlockBar[4];
                blockOneFull = false;
                break;
            case 7:
                playerBlockBarSpriteRenderer1.sprite = playerBlockBar[3];
                blockOneFull = false;
                break;
            case 8:
                playerBlockBarSpriteRenderer1.sprite = playerBlockBar[2];
                blockOneFull = false;
                break;
            case 9:
                playerBlockBarSpriteRenderer1.sprite = playerBlockBar[1];
                blockOneFull = false;
                break;
            case 10:
                playerBlockBarSpriteRenderer1.sprite = playerBlockBar[0];
                blockOneFull = true;
                canBlock = true;
                break;
        }
        switch (playerBlockLevelTwo)
        {
            case 0:
                playerBlockBarSpriteRenderer2.sprite = playerBlockBar[10];
                blockTwoFull = false; 
                break;
            case 1:
                playerBlockBarSpriteRenderer2.sprite = playerBlockBar[9];
                blockTwoFull = false;
                break;
            case 2:
                playerBlockBarSpriteRenderer2.sprite = playerBlockBar[8];
                blockTwoFull = false;
                break;
            case 3:
                playerBlockBarSpriteRenderer2.sprite = playerBlockBar[7];
                blockTwoFull = false;
                break;
            case 4:
                playerBlockBarSpriteRenderer2.sprite = playerBlockBar[6];
                blockTwoFull = false;
                break;
            case 5:
                playerBlockBarSpriteRenderer2.sprite = playerBlockBar[5];
                blockTwoFull = false;
                break;
            case 6:
                playerBlockBarSpriteRenderer2.sprite = playerBlockBar[4];
                blockTwoFull = false;
                break;
            case 7:
                playerBlockBarSpriteRenderer2.sprite = playerBlockBar[3];
                blockTwoFull = false;
                break;
            case 8:
                playerBlockBarSpriteRenderer2.sprite = playerBlockBar[2];
                blockTwoFull = false;
                break;
            case 9:
                playerBlockBarSpriteRenderer2.sprite = playerBlockBar[1];
                blockTwoFull = false;
                break;
            case 10:
                playerBlockBarSpriteRenderer2.sprite = playerBlockBar[0]; 
                blockTwoFull = true;
                canBlock = true;
                break;
        }
        switch (playerBlockLevelThree)
        {
            case 0:
                playerBlockBarSpriteRenderer3.sprite = playerBlockBar[10];
                blockThreeFull = false;
                break;
            case 1:
                playerBlockBarSpriteRenderer3.sprite = playerBlockBar[9];
                blockThreeFull = false;
                break;
            case 2:
                playerBlockBarSpriteRenderer3.sprite = playerBlockBar[8];
                blockThreeFull = false;
                break;
            case 3:
                playerBlockBarSpriteRenderer3.sprite = playerBlockBar[7];
                blockThreeFull = false;
                break;
            case 4:
                playerBlockBarSpriteRenderer3.sprite = playerBlockBar[6];
                blockThreeFull = false;
                break;
            case 5:
                playerBlockBarSpriteRenderer3.sprite = playerBlockBar[5];
                blockThreeFull = false;
                break;
            case 6:
                playerBlockBarSpriteRenderer3.sprite = playerBlockBar[4];
                blockThreeFull = false;
                break;
            case 7:
                playerBlockBarSpriteRenderer3.sprite = playerBlockBar[3];
                blockThreeFull = false;
                break;
            case 8:
                playerBlockBarSpriteRenderer3.sprite = playerBlockBar[2];
                blockThreeFull = false;
                break;
            case 9:
                playerBlockBarSpriteRenderer3.sprite = playerBlockBar[1];
                blockThreeFull = false;
                break;
            case 10:
                playerBlockBarSpriteRenderer3.sprite = playerBlockBar[0];
                blockThreeFull = true;
                canBlock = true; 
                break;
        }
    }
}
