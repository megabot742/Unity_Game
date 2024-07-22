using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
   [SerializeField] bool isPlayer;
   [SerializeField] int health = 50;
   [SerializeField] int score = 50;
   [SerializeField] ParticleSystem hitEffect;
   [SerializeField] bool applyCameraShake;
   CameraShake cameraShake;
   AudioPlayer audioPlayer;
   ScoreKeeper scoreKeeper;
   void Awake()
   {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
   }
   public int GetHealth()
   {
        return health;
   }
   void OnTriggerEnter2D(Collider2D other) 
   {
        DamgeDealer damgeDealer = other.GetComponent<DamgeDealer>();
        if(damgeDealer !=null)
        {
            TakeDamage(damgeDealer.GetDamage());
            PlayHitEffect();
            AudioHit();
            ShakeCamera();
            damgeDealer.Hit();
        }
   }
    void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        if(!isPlayer)
        {
            scoreKeeper.ModifyScore(score);
        }  
        Destroy(gameObject);
    }
    void PlayHitEffect()
    {
        if(hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }
    void ShakeCamera()
    {
        if(cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }
    void AudioHit()
    {
        if(audioPlayer != null)
        {
            audioPlayer.PlayDamageClip();
        }
    }
}
