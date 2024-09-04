using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Target"))
        {
            print("Hit " + other.gameObject.name + "!");
            Destroy(gameObject);
        }    
        if(other.gameObject.CompareTag("Wall"))
        {
            print("Hit a wall");
            Destroy(gameObject);
        }
    }
}
