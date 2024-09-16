using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Vector3 camFollow;
    Transform ball, Win;

    void Awake() 
    {
        ball = FindObjectOfType<Ball>().transform;
    }
    void Update()
    {
        //check camera
        if(Win == null)
        {
            Win = GameObject.Find("Win(Clone)").GetComponent<Transform>();
        }
        //move camera with ball
        if(transform.position.y > ball.transform.position.y && transform.position.y > Win.position.y + 4f)
            camFollow = new Vector3(transform.position.x, ball.position.y, transform.position.z);
        
        transform.position = new Vector3(transform.position.x, camFollow.y, -5);
    }
}
