using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;   //First person camera
    [SerializeField] float range = 100f; //weapon range
    [SerializeField] float damage = 25f;
    [SerializeField] ParticleSystem muzzleFlash;
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        PlayMuzzleFlash();
        ProcessRayCast();
    }
    void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }
    void ProcessRayCast()
    {
        RaycastHit hit;
        if(Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            Debug.Log("hit" + hit.transform.name);
            //TODO: add some hit effect for visual player
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            //call a method on EnemyHelth that decreases the enemy's health
            if(target == null) return;
            target.TakeDamage(damage);
        }
        else
        {
            return; //hit sky
        }
    }
}
