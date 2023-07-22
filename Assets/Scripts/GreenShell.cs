using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//------------------------------------------------------------------------------
// QUIZ 
// WRITTEN BY: JEREMY POULIN (40112762)
// FOR COMP376 - FALL 2022
//------------------------------------------------------------------------------

public class GreenShell : MonoBehaviour
{
    private Rigidbody rigidbodyRef;
    private PlayerControls playerRef;
    private GameObject player;

    //Shell variables
    [SerializeField] private float shellSpeed = 10f;
    [SerializeField] private float shellTimer = 15f;
    private float time = 0f;
    private int bounceCount = 0;
    private bool thrown = false;
    private Vector3 forward;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyRef = gameObject.GetComponent<Rigidbody>();
        playerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
        player = GameObject.FindGameObjectWithTag("Player");

        forward = player.transform.TransformDirection(Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        if (!thrown)
        {
            rigidbodyRef.AddForce(forward * shellSpeed, ForceMode.Impulse);
        }

        time += Time.deltaTime;

        if (time > shellTimer)
        {
            Destroy(this.gameObject);
        }
    }
}
