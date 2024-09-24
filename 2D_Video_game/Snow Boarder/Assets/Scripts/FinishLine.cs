using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] float LoadDeplay = 1f;
    [SerializeField] ParticleSystem finishEffect;
    
    private void OnTriggerEnter2D(Collider2D other) //check player trigger
    {
        if(other.tag == "Player")
        {
            finishEffect.Play();
            GetComponent<AudioSource>().Play();
            Invoke("ReloadScene", LoadDeplay);
        }
    }

    void ReloadScene() //after trigger finish reload scene
    {
        SceneManager.LoadScene(0);
    }
}
