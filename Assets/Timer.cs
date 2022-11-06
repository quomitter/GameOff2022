using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Timer : MonoBehaviour
{
    public TMP_Text timer;
    public float countDownTimer;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        countDownTimer = 60.0f;
        timer.text = "60";
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.gameIsActive)
        {
            countDownTimer -= Time.deltaTime;
            timer.text = System.Convert.ToString(Mathf.RoundToInt(countDownTimer));
        }
    }
}
