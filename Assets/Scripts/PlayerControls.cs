using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//------------------------------------------------------------------------------
// QUIZ 
// WRITTEN BY: JEREMY POULIN (40112762)
// FOR COMP376 - FALL 2022
//------------------------------------------------------------------------------

public class PlayerControls : MonoBehaviour
{
    //Movement variables
    [HideInInspector] public float currentSpeed = 0f;
    public float acceleration = 5f;
    public float topSpeed = 25f;

    public float driftingFraction = 0.9f;
    public float grassFraction = 0.7f;
    public float sandFraction = 0.5f;

    public float gravity = 20.0f;
    public float turnSpeed = 5.0f;
    public float lookSpeed = 2.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;

    //Shell Prefab
    [SerializeField] private GameObject shellItem;

    //Items variables
    private bool hasShell = false;
    private bool hasCoin = false;
    private float coinSpeed = 1.5f;
    private float coinTimer = 3.0f;
    private float time = 0f;
    private int coinCounter = 0;

    //Camera variables
    [SerializeField] private GameObject cameraRef;
    private bool cameraChanged = false;

    [HideInInspector] public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //Change Perspective if key T is pressed
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!cameraChanged)
            {
                cameraChanged = true;
                cameraRef.SetActive(true);
            }
            else
            {
                cameraChanged = false;
                cameraRef.SetActive(false);
            }
        }

        //Check if the coin has expired
        if (hasCoin)
        {
            time += Time.deltaTime;

            if (time > coinTimer)
            {
                time = 0f;
                hasCoin = false;
            }
        }

        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        // Press Left Shift to drift
        bool isDrifting = Input.GetKey(KeyCode.LeftShift);

        //Increase the acceleration of the player as time increases
        if (canMove)
        {
            if (Input.GetAxis("Vertical") == 1)
            {
                if (currentSpeed < topSpeed)
                {
                    currentSpeed += acceleration * Time.deltaTime;
                }
            }
            else if (Input.GetAxis("Vertical") == 0)
            {
                if (currentSpeed > 0)
                {
                    currentSpeed -= acceleration * Time.deltaTime;
                }
            }
            else if (Input.GetAxis("Vertical") == -1)
            {
                if (currentSpeed > -topSpeed)
                {
                    currentSpeed -= acceleration * Time.deltaTime;
                }
            }
        }

        //Calculate speed variation fraction
        float slowDownValue = (isDrifting ? driftingFraction : 1f) * (hasCoin ? coinSpeed : 1f) * SlowDownFraction();

        moveDirection = (forward * currentSpeed * slowDownValue);

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.rotation *= Quaternion.Euler(0, -turnSpeed * lookSpeed, 0);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.rotation *= Quaternion.Euler(0, turnSpeed * lookSpeed, 0);
            }
        }

        //Player throws a shell if they have one in their inventory
        if (hasShell && Input.GetKeyDown(KeyCode.Space))
        {
            ThrowGreenShell();
            hasShell = false;
        }
    }

    //Check if the current track should slow the player down (return the percentage of new top speed)
    public float SlowDownFraction()
    {
        Vector3 startOfRay = transform.TransformPoint(characterController.center);
        float lengthOfRay = characterController.center.y + 5.0f;

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
                return grassFraction;
            }
            else if (col.CompareTag("Sand"))
            {
                return sandFraction;
            }
            else if (col.CompareTag("OutOfBounds"))
            {
                return 0;
            }
        }

        return 1;
    }

    //Throw Shell Function
    public void ThrowGreenShell()
    {
        Instantiate(shellItem, gameObject.transform);
    }

    //Update coin counter
    public void UpdateCoinCounter()
    {
        coinCounter++;
    }

    //Restart coin timer if the player gets another coin while holding one still
    public void ResetCoinTimer()
    {
        time = 0f;
    }

    //Get and Set functions for player variables
    public int GetCoinCounter()
    {
        return coinCounter;
    }

    public void SetHasCoin(bool x)
    {
        this.hasCoin = x;
    }

    public bool GetHasCoin()
    {
        return hasCoin;
    }

    public void SetHasShell(bool x)
    {
        this.hasShell = x;
    }

    public bool GetHasShell()
    {
        return hasShell;
    }

    public void SetCanMove(bool x)
    {
        this.canMove = x;
    }
}
