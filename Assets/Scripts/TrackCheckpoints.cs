using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//------------------------------------------------------------------------------
// QUIZ 
// WRITTEN BY: JEREMY POULIN (40112762)
// FOR COMP376 - FALL 2022
//------------------------------------------------------------------------------

public class TrackCheckpoints : MonoBehaviour
{
    //List of chekpoints
    private List<Checkpoints> checkpointList = new List<Checkpoints>();
    private int nextCheckpointIndex;

    //Lap variables
    private int currentLap = 0;

    //Wrong Way variables
    [SerializeField] private GameObject wrongWay;

    //Finished
    [SerializeField] private UIChanges UIChangesRef;

    // Start is called before the first frame update
    void Awake()
    {
        Transform checkpointsTransform = GameObject.FindGameObjectWithTag("Checkpoints").GetComponent<Transform>();

        foreach (Transform checkpointTransform in checkpointsTransform)
        {
            Checkpoints checkpoint = checkpointTransform.GetComponent<Checkpoints>();
            checkpoint.SetTrackCheckpointsRef(this);

            checkpointList.Add(checkpoint);
        }

        //Set the next checkpoint to be zero (so as to not go around the endline)
        nextCheckpointIndex = 0;
    }

    public void MarioThroughCheckpoint(Checkpoints checkpoint)
    {
        if (checkpointList.IndexOf(checkpoint) == nextCheckpointIndex)
        {
            //Correct checkpoint passed, update the nextCheckpoint value
            wrongWay.SetActive(false);
            nextCheckpointIndex = (nextCheckpointIndex + 1) % checkpointList.Count;

            //Update Lap completed UI
            if (nextCheckpointIndex == 1 && currentLap != 3)
            {
                //Lap completed
                currentLap++;
            }
            else if (nextCheckpointIndex == 1 && currentLap == 3)
            {
                //Race finished
                UIChangesRef.RaceFinished("YOU WON!!");
            }
        }
        else
        {
            //Wrong Checkpoint passed
            wrongWay.SetActive(true);
        }
    }

    public int GetCurrentLap()
    {
        return currentLap;
    }
}
