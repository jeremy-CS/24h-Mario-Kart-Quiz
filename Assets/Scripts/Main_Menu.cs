using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//------------------------------------------------------------------------------
// QUIZ 
// WRITTEN BY: JEREMY POULIN (40112762)
// FOR COMP376 - FALL 2022
//------------------------------------------------------------------------------

public class Main_Menu : MonoBehaviour
{
    //UI gameobject references
    [SerializeField] private GameObject Press_space;
    [SerializeField] private GameObject Map_select;

    void Update()
    {
        //Show the map options when the player presses space
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Press_space.SetActive(false);
            Map_select.SetActive(true);
        }
    }
}
