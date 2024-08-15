using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    
    [SerializeField] int scorePerHit = 25;
    [SerializeField] int hitPoint = 75;

    ScoreBoard scoreBoard;
    Rigidbody rb;
    GameObject parentGameObject;
    void Start() 
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        AddRigidbody();
    }
    void AddRigidbody()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }
    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if(hitPoint < 1)
        {
            KillEnemy();
        }
    }
    void ProcessHit()
    {
         GameObject hitvfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        hitvfx.transform.parent = parentGameObject.transform;
        hitPoint--;
        
    }
    void KillEnemy() 
    {
        scoreBoard.IncreaseScore(scorePerHit);
        //Debug.Log($"{name} hit by {other.gameObject.name}"); //check enemy hit by player ship
        GameObject deathvfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        deathvfx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }
}
