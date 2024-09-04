using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 500f;

    float xRotation = 0f;
    float yRotation = 0f;

    [SerializeField] float topClamp = -90f;
    [SerializeField] float bottomClamp = 90f;
    void Start()
    {
        //Locking the cursor to the middle of the screen and making it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Getting the mouse inputs
        float  mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float  mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Rotation round the x axis (Look up and down)
        xRotation -= mouseY;

        //Clamp the rotation 
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);

        //Rotation round the y axis (Look left and right)
        yRotation += mouseX;

        //Apply rotations to our tranfrom
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
