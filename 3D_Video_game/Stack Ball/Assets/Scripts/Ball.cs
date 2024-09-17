using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
    float currentTime;
    bool smash, invincible;
    public enum BallState
    {
        Prepare,
        Playing,
        Died,
        Finish
    }

    [HideInInspector]
    public BallState ballState = BallState.Prepare;

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
        if(ballState == BallState.Playing)
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

            if(invincible)
            {
                currentTime -= Time.deltaTime * 0.35f;
            }
            else
            {
                if(smash)
                {
                    currentTime += Time.deltaTime * 0.8f;
                }
                else
                {
                    currentTime -= Time.deltaTime * 0.5f;
                }
            }

            if(currentTime >= 1)
            {
                currentTime = 1;
                invincible = true;
            }
            else if(currentTime >= 1)
            {
                currentTime = 0;
                invincible = false;
            }
        }
        if(ballState == BallState.Prepare)
        {
            if(Input.GetMouseButtonDown(0))
            {
                ballState = BallState.Playing;
            }
        }
        if(ballState == BallState.Finish)
        {
            if(Input.GetMouseButtonDown(0))
            {
                FindObjectOfType<LevelSpawner>().NextLevel();
            }
        }
    }
    void FixedUpdate() //FixedUpdate is often used to handle physics-related tasks
    {
        if(ballState == BallState.Playing)
        {
            if(Input.GetMouseButton(0))
            {
                smash = true;
                rb.velocity = new Vector3(0, -100 * Time.fixedDeltaTime * 7, 0);
            }  
        }
        if(rb.velocity.y > 5) // maximum velocity
        {
            rb.velocity = new Vector3(rb.velocity.x, 5, rb.velocity.z);
        }
    }
    public void IncreaseBronkenStacks()
    {
        if(!invincible)
        {
            ScoreManager.instance.AddScore(1);
        }
        else
        {
            ScoreManager.instance.AddScore(2);
        }
    }
    void OnCollisionEnter(Collision other) 
    {
        if(!smash)
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5,0);
        }
        else
        {
            if(invincible)
            {
                if(other.gameObject.tag == "enemy" || other.gameObject.tag == "plane")
                {
                    other.transform.parent.GetComponent<StackController>().ShatterAllPart();
                }
            }
            else
            {
                if(other.gameObject.tag == "enemy")
                {
                    other.transform.parent.GetComponent<StackController>().ShatterAllPart();
                }
                else if(other.gameObject.tag == "plane")
                {
                    Debug.Log("over");
                }
            }
        }

        if(other.gameObject.tag == "Finish" && ballState == BallState.Playing)
        {
            ballState = BallState.Finish;
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
