using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//------------------------------------------------------------------------------
// QUIZ 
// WRITTEN BY: JEREMY POULIN (40112762)
// FOR COMP376 - FALL 2022
//------------------------------------------------------------------------------

public class Checkpoints : MonoBehaviour
{
    private TrackCheckpoints trackCheckpointsRef;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trackCheckpointsRef.MarioThroughCheckpoint(this);
        }
    }

    public void SetTrackCheckpointsRef(TrackCheckpoints trackCheckpoints)
    {
        this.trackCheckpointsRef = trackCheckpoints;
    }
}
