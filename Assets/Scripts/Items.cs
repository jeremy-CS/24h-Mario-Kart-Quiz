using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//------------------------------------------------------------------------------
// QUIZ 
// WRITTEN BY: JEREMY POULIN (40112762)
// FOR COMP376 - FALL 2022
//------------------------------------------------------------------------------

public class Items : MonoBehaviour
{
    private PlayerControls playerRef;

    //Timer variables
    private bool startTimer = false;
    private float time = 0f;
    [SerializeField] private float itemRespawnTimer = 30f;

    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
    }

    private void Update()
    {
        if (startTimer)
        {
            time += Time.deltaTime;

            if (time > itemRespawnTimer)
            {
                time = 0f;
                startTimer = false;

                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //If coin, collected, update speed for three seconds and delete coin
            if (gameObject.CompareTag("Coins"))
            {
                playerRef.UpdateCoinCounter();
                gameObject.GetComponent<AudioSource>().Play();
                Destroy(gameObject, 0.3f);

                if (playerRef.GetHasCoin())
                {
                    //If player already has coin, restart timer
                    playerRef.ResetCoinTimer();
                }
                else
                {
                    //Start coin timer for player
                    playerRef.SetHasCoin(true);
                }
            }
            //If item box collected, give green shell and turn off for 30 seconds
            else if (gameObject.CompareTag("Item_box") && !startTimer)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                startTimer = true;

                if (!playerRef.GetHasShell())
                {
                    playerRef.SetHasShell(true);
                }
            }
        }
    }

}
