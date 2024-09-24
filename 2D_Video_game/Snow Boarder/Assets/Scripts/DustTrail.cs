using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustTrail : MonoBehaviour
{
    [SerializeField] ParticleSystem dustEffect;

    void OnCollisionEnter2D(Collision2D other) {//turn on effect when player on ground
        if (other.gameObject.tag == "Ground")
        {
            dustEffect.Play();
        }
    }
    void OnCollisionExit2D(Collision2D other) { //turn off effect when player in the air
        if (other.gameObject.tag == "Ground")
        {
            dustEffect.Stop();
        } 
    }
}
