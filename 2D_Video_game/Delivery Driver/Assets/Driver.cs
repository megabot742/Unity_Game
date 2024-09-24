using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] float steerSpeed = 0.2f; 
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float slowSpeed = 15f;
    [SerializeField] float boostSpeed = 30f;

    // Update is called once per frame
    void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;// move left and right
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime; // move up and down
        transform.Rotate(0, 0, -steerAmount);
        transform.Translate(0, moveAmount, 0);
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        //decreased speed after crash
        moveSpeed = slowSpeed; 
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        //increase speed when hit boost
        if(other.tag == "Boost")
        {
            moveSpeed = boostSpeed;
        }
    }
}
