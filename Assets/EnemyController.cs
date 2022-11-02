using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public int randomNumber; 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        randomNumber = Random.Range(1, 4);

        switch (randomNumber)
        {
            case 1:
                //move toward player
                break;
            case 2:
                //Kick
                break;
            case 3:
                //Punch
                break;
        }
    }


}
