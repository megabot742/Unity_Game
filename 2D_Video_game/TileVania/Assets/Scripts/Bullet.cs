using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float bulletSpeed = 10f;
    Rigidbody2D myRigibody;
    PlayerMovement player;
    float xSpeed;
    void Start()
    {
        myRigibody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }
    // Update is called once per frame
    void Update()
    {    
        myRigibody.velocity = new Vector2(xSpeed, 0f);        
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
       Destroy(gameObject); 
    }
}
