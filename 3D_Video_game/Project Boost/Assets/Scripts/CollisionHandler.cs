using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDeplay = 2f;
    [SerializeField] AudioClip crashAduio;
    [SerializeField] AudioClip finishAduio;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem finishParticles;

    AudioSource audioSource;

    bool isTransitionting;
    bool collisionDisabled;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    void Update() 
    {
        RespondToDebugKeys();
    }
    void RespondToDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; //toggle collision
        }
    }
    void OnCollisionEnter(Collision other) 
    {
        if(isTransitionting || collisionDisabled)
        {
            return;
        }
        switch (other.gameObject.tag) //else
        {
            case "Launch":
                Debug.Log("Rocket ready for launch");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                Debug.Log("Rocket crash!!!, Try again");
                StartCrashSequence();
                break;
        }
    }
    void StartSuccessSequence()
    {
        isTransitionting = true;
        audioSource.Stop(); //stop all sound before play finishAduio 
        audioSource.PlayOneShot(finishAduio);
        finishParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDeplay); //Invoke -> Method deplay time
    }
    void StartCrashSequence()
    {
        isTransitionting = true;
        audioSource.Stop(); //stop all sound before play crashAduio
        audioSource.PlayOneShot(crashAduio);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDeplay);
        
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex); 
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    
}
