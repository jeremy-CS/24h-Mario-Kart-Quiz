using UnityEngine;
using System.Collections;

//------------------------------------------------------------------------------
// QUIZ 
// WRITTEN BY: JEREMY POULIN (40112762)
// FOR COMP376 - FALL 2022
//------------------------------------------------------------------------------

public class MouseControl : MonoBehaviour
{
    [SerializeField] float mouseSensitivity;

    [SerializeField] PlayerUnit unit;

    [SerializeField]

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Left-click or middle-click to drag camera around
        if (Input.GetMouseButton(0) || Input.GetMouseButton(2))
        {
            transform.Translate(new Vector3(-mouseX, 0.0f, -mouseY) * mouseSensitivity, Space.World);
        }

        // Right-click to move unit
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                unit.SetDestination(hit.point);
                //Instantiate (markerPrefab, hit.point + new Vector3(0.0f, 0.1f, 0.0f), markerPrefab.transform.rotation);

            }
        }
    }
}
