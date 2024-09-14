using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;   //First person camera
    [SerializeField] float range = 100f; //weapon range
    [SerializeField] float damage = 25f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;

    [Header("Ammo")]
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;

    [Header("Deplay")]
    [SerializeField] bool canShot = true;
    [SerializeField] float timeBetweenShots = 0.5f; 

    [Header("Text")]
    [SerializeField] TextMeshProUGUI ammoText;

    private void OnEnable() 
    {
        canShot = true;
    }
    void Update()
    {
        DisplayAmmo();
        if((Input.GetKey(KeyCode.Mouse0) && canShot == true) || (Input.GetKeyDown(KeyCode.Mouse0) && canShot == true)) //holding left mouse or click left mouse
        {
            StartCoroutine(Shoot());
        }
    }
    void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = "Bullets: " + currentAmmo.ToString();
    }

    IEnumerator Shoot()
    {   canShot = false; 
        if(ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            PlayMuzzleFlash();
            ProcessRayCast();
            ammoSlot.ReduceCurrentAmmo(ammoType);    
        }
        yield return new WaitForSeconds(timeBetweenShots); 
        canShot = true;
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
            //TODO: add some hit effect for visual player
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if(target == null) return;
            target.TakeDamage(damage);
        }
        else
        {
            return; //hit sky
        }
    }
    void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        //Destroy(impact, 1);
    }
}
