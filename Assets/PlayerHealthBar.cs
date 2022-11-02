using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    public SpriteRenderer playerSpriteRenderer;
    public Sprite[] playerHealthBar;
    public int playerHealthLevel;

    // Start is called before the first frame update
    void Start()
    {
        playerSpriteRenderer.sprite = playerHealthBar[10];
    }

    // Update is called once per frame
    void Update()
    {
        switch (playerHealthLevel)
        {
            case 0:
                playerSpriteRenderer.sprite = playerHealthBar[0];
                break;
            case 1:
                playerSpriteRenderer.sprite = playerHealthBar[1];
                break;
            case 2:
                playerSpriteRenderer.sprite = playerHealthBar[2];
                break;
            case 3:
                playerSpriteRenderer.sprite = playerHealthBar[3];
                break;
            case 4:
                playerSpriteRenderer.sprite = playerHealthBar[4];
                break;
            case 5:
                playerSpriteRenderer.sprite = playerHealthBar[5];
                break;
            case 6:
                playerSpriteRenderer.sprite = playerHealthBar[6];
                break;
            case 7:
                playerSpriteRenderer.sprite = playerHealthBar[7];
                break;
            case 8:
                playerSpriteRenderer.sprite = playerHealthBar[8];
                break;
            case 9:
                playerSpriteRenderer.sprite = playerHealthBar[9];
                break;
            case 10:
                playerSpriteRenderer.sprite = playerHealthBar[10];
                break;
        }
    }
}
