using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//------------------------------------------------------------------------------
// QUIZ 
// WRITTEN BY: JEREMY POULIN (40112762)
// FOR COMP376 - FALL 2022
//------------------------------------------------------------------------------

public class Boo : MonoBehaviour
{
    //Waypoints variables
    [SerializeField] private GameObject[] waypoints;
    private int nextWaypoint = 0;
    public float rotationSpeed = 10f;
    public float waypointRadius = 1f;

    //Movement variables
    private float currentSpeed = 0f;
    private float maxSpeed = 25f;
    private float acceleration = 10f;
    private bool canMove = false;

    //Laps variables
    private int currentLap = 0;
    [SerializeField] private UIChanges UIChangesRef;

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            //Slowdown ratio
            float slowdownRatio = SlowDownFraction();

            //Update Boo's speed
            if (currentSpeed < maxSpeed)
            {
                currentSpeed += acceleration * Time.deltaTime * slowdownRatio;
            }

            Race();
        }
    }

    //Check if the current track should slow the player down (return the percentage of new top speed)
    public float SlowDownFraction()
    {
        Vector3 startOfRay = transform.TransformPoint(transform.position);
        float lengthOfRay = transform.position.y + 5.0f;

        bool hit = Physics.Raycast(startOfRay, Vector3.down, out RaycastHit hitInfo, lengthOfRay);
        Collider col = hitInfo.collider;

        //Check what type of ground the ray hit, and return the appropriate slow down value
        if (col != null)
        {
            if (col.CompareTag("Track"))
            {
                return 1;
            }
            else if (col.CompareTag("Grass"))
            {
                return 0.7f;
            }
            else if (col.CompareTag("Sand"))
            {
                return 0.5f;
            }
            else if (col.CompareTag("OutOfBounds"))
            {
                return 0;
            }
        }

        return 1;
    }

    public void Race()
    {
        if (Vector3.Distance(waypoints[nextWaypoint].transform.position, transform.position) < waypointRadius)
        {
            nextWaypoint = (nextWaypoint + 1) % waypoints.Length;

            //Update Lap completed UI
            if (nextWaypoint == 1 && currentLap != 3)
            {
                //Lap completed
                currentLap++;
            }
            else if (nextWaypoint == 1 && currentLap == 3)
            {
                //Race finished
                UIChangesRef.RaceFinished("YOU LOST..");
            }
        }

        //Move Boo to the next waypoint
        //booRef.SetDestination(waypoints[currentWaypoint].transform.position);
        transform.position = Vector3.MoveTowards(transform.position, waypoints[nextWaypoint].transform.position, currentSpeed * Time.deltaTime);
    }

    public int GetCurrentLap()
    {
        return currentLap;
    }

    public void SetCanMove(bool x)
    {
        canMove = x;
    }
}