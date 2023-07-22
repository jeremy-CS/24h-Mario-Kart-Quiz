using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//------------------------------------------------------------------------------
// QUIZ 
// WRITTEN BY: JEREMY POULIN (40112762)
// FOR COMP376 - FALL 2022
//------------------------------------------------------------------------------

public class Rotation : MonoBehaviour
{
    [SerializeField] float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        //Rotate around yourself (for coins and item boxes)
        this.gameObject.transform.RotateAround(this.gameObject.transform.position, Vector3.up, rotationSpeed);
    }
}
