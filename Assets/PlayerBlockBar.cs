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

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerBlockBarSpriteRenderer1.sprite = playerBlockBar[0];
        playerBlockBarSpriteRenderer2.sprite = playerBlockBar[0];
        playerBlockBarSpriteRenderer3.sprite = playerBlockBar[0];
        blockOneFull = true;
        blockTwoFull = true;
        blockThreeFull = true;
        playerBlockRechargeRate = 0.2f;
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
                playerBlockRechargeRate = 0.2f;
                if (playerBlockLevelOne == 10)
                {
                    blockOneFull = true;
                }
                    
            }
            if (!blockTwoFull)
            {
                if (playerBlockLevelTwo <= 9)
                    playerBlockLevelTwo++;
                playerBlockRechargeRate = 0.2f;
                if (playerBlockLevelTwo == 10)
                {
                    blockTwoFull = true;  
                }
                    
            }
            if (!blockThreeFull)
            {
                if (playerBlockLevelThree <= 9)
                    playerBlockLevelThree++;
                playerBlockRechargeRate = 0.2f;
                if (playerBlockLevelThree == 10)
                {
                    blockThreeFull = true;  
                }
                    
            }
        }

        if (playerController.isBlocking)
        {
            if (blockOneFull && blockTwoFull && blockThreeFull)
            {
                playerBlockLevelThree = 0;
                playerController.blockTimer = 3;
                canBlock = true; 
            }
            else if (blockOneFull && blockTwoFull && !blockThreeFull)
            {
                playerBlockLevelTwo = 0;
                playerController.blockTimer = 3;
                canBlock = true;
            }
            else if (blockOneFull && !blockTwoFull && !blockThreeFull)
            {
                playerBlockLevelOne = 0;
                playerController.blockTimer = 3;
                canBlock = true;
            }
            else if (!blockOneFull && !blockTwoFull && !blockThreeFull)
            {
                canBlock = false;
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
