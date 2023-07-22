using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//------------------------------------------------------------------------------
// QUIZ 
// WRITTEN BY: JEREMY POULIN (40112762)
// FOR COMP376 - FALL 2022
//------------------------------------------------------------------------------

public class CountDown : MonoBehaviour
{
    //Player Reference
    [SerializeField] private PlayerControls playerRef;
    [SerializeField] private Boo booRef;
    [SerializeField] private Text countDownText;

    void Start()
    {
        StartCoroutine(Countdown(4));
    }

    IEnumerator Countdown(int seconds)
    {
        int count = seconds;
        playerRef.canMove = false;
        booRef.SetCanMove(false);

        while (count > 0)
        {
            if (count == 1)
            {
                // count down is finished
                StartGame();
                countDownText.text = "GO!!";
            }
            else
            {
                //Show countdown
                countDownText.text = (count - 1).ToString() + "..";
            }

            yield return new WaitForSeconds(1);
            count--;
        }

        this.gameObject.SetActive(false);
    }

    void StartGame()
    {
        Time.timeScale = 1;
        playerRef.canMove = true;
        booRef.SetCanMove(true);
    }

}
