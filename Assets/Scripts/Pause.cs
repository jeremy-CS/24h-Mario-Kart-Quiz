using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//------------------------------------------------------------------------------
// QUIZ 
// WRITTEN BY: JEREMY POULIN (40112762)
// FOR COMP376 - FALL 2022
//------------------------------------------------------------------------------

public class Pause : MonoBehaviour
{
    public GameObject pauseUI;
    private PlayerControls playerRef;

    // Start is called before the first frame update
    void Start()
    {
        if (Time.timeScale == 0) Time.timeScale = 1;

        playerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
    }

    // Check if the player wants to pause the game with key P
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //Restrict the player movements
            playerRef.SetCanMove(false);

            //Pause game and unlock/show cursor
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pauseUI.SetActive(true);
        }
    }

    // Continue the game after being paused
    public void Resume()
    {
        //Give back control to player movement
        playerRef.SetCanMove(true);

        //Unpause game and hide/lock cursor
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseUI.SetActive(false);
    }
}
