using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//------------------------------------------------------------------------------
// QUIZ 
// WRITTEN BY: JEREMY POULIN (40112762)
// FOR COMP376 - FALL 2022
//------------------------------------------------------------------------------

public class UIChanges : MonoBehaviour
{
    //Player based variables
    private int coinCounter;
    private bool hasShell;
    private PlayerControls playerRef;
    [SerializeField] private Text coinText;
    [SerializeField] private GameObject noItemIcon;
    [SerializeField] private GameObject shellIcon;

    //Track based variables
    private int currentLapMario;
    private int currentLapBoo;
    [SerializeField] private Boo booRef;
    [SerializeField] private TrackCheckpoints trackCheckpoints;
    [SerializeField] private Text currentLapTextMario;
    [SerializeField] private Text currentLapTextBoo;

    //Race finished variables
    [SerializeField] private GameObject finishUI;
    [SerializeField] private Text resultsText;
    private AudioSource[] raceFinishedSounds;
    private AudioSource crossingFinishLine;
    private AudioSource resultsLoop;
    [SerializeField] private AudioSource levelTheme;

    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();

        raceFinishedSounds = finishUI.GetComponents<AudioSource>();
        crossingFinishLine = raceFinishedSounds[0];
        resultsLoop = raceFinishedSounds[1];
    }

    // Update is called once per frame
    void Update()
    {
        //Fetch information about the game
        coinCounter = playerRef.GetCoinCounter();
        hasShell = playerRef.GetHasShell();
        currentLapMario = trackCheckpoints.GetCurrentLap();
        currentLapBoo = booRef.GetCurrentLap();

        //Update information
        ChangeCoinCount();
        ChangeShellIcon();
        ChangeCurrentLap();
    }

    public void ChangeCoinCount()
    {
        coinText.text = coinCounter.ToString();
    }

    public void ChangeShellIcon()
    {
        if (hasShell)
        {
            noItemIcon.SetActive(false);
            shellIcon.SetActive(true);
        }
        else
        {
            shellIcon.SetActive(false);
            noItemIcon.SetActive(true);
        }
    }

    public void ChangeCurrentLap()
    {
        if (currentLapMario != 0)
        {
            currentLapTextMario.text = currentLapMario.ToString() + "/3";
        }

        if (currentLapBoo != 0)
        {
            currentLapTextBoo.text = currentLapBoo.ToString() + "/3";
        }
    }

    public void RaceFinished(string winnerResults)
    {
        //Freeze game, and show finished UI
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerRef.SetCanMove(false);
        finishUI.SetActive(true);

        //Update message to let the player know the results of the game
        resultsText.text = winnerResults;

        //Play soundtrack for finishing the race (stop level theme)
        levelTheme.mute = true;

        crossingFinishLine.Play();
        resultsLoop.PlayDelayed(crossingFinishLine.clip.length);
    }
}
