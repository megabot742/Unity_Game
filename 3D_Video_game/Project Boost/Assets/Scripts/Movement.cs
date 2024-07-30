using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    Rigidbody myRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            //Vector3.back = (0,1,0)
            myRigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        } 
    }
    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            //Vector3.forward = (0,0,1)
            ApplyRotation(rotationThrust);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            //Vector3.back = (0,0,-1)
            ApplyRotation(-rotationThrust);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        myRigidbody.freezeRotation = true; //freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        myRigidbody.freezeRotation = false; //unfreezing rotation so the physics system can take over
    }
}
