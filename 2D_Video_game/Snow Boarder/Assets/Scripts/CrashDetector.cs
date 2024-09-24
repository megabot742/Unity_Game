using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] float LoadDeplay = 0.5f;
    [SerializeField] ParticleSystem crashEffect;
    [SerializeField] AudioClip crashSFX;
    bool hasCrasher = false;
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Ground" && !hasCrasher) //check crash, disable player control and reload scene
        {
            hasCrasher = true;
            FindObjectOfType<PlayerController>().DisableControls();
            crashEffect.Play();
            GetComponent<AudioSource>().PlayOneShot(crashSFX);
            Invoke("ReloadScene", LoadDeplay);         
        }    
    }
    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
