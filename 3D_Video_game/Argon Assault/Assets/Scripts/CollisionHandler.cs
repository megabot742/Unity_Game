using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDeplay = 1f;
    [SerializeField] ParticleSystem explosion;
   
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(this.name + "--Trigger with--" + other.gameObject.name);
        StartCrashSequence();
    }
    void StartCrashSequence()
    {
        explosion.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        Invoke("ReloadLevel", levelLoadDeplay); 
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
