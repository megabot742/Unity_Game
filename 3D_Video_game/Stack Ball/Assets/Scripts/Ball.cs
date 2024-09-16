using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
    bool smash;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //click mouse left
        if(Input.GetMouseButtonDown(0))
        {
            smash = true;
        }
        //click mouse right
        else if(Input.GetMouseButtonDown(1))
        {
            smash = false;
        }
    }
    void FixedUpdate() //FixedUpdate is often used to handle physics-related tasks
    {
        if(Input.GetMouseButton(0))
        {
            smash = true;
            rb.velocity = new Vector3(0, -100 * Time.fixedDeltaTime * 7, 0);
        }  
        if(rb.velocity.y > 5) // maximum velocity
        {
            rb.velocity = new Vector3(rb.velocity.x, 5, rb.velocity.z);
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if(!smash)
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
        }
        else //hit the model
        {
            if(other.gameObject.tag == "enemy") // another color
            {
                Destroy(other.transform.parent.gameObject);
            }
            if(other.gameObject.tag == "plane") // color black 
            {
                Debug.Log("Over");
            }
        }
    }
    void OnCollisionStay(Collision other)
    {
        if(!smash || other.gameObject.tag == "Finish") // check for finish 
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0); //reset velocity to normal
        }
    }
}
