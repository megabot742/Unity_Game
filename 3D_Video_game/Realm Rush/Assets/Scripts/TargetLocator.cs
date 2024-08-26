using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem projectileParticles;
    [SerializeField] float range = 15f;
    Transform target;
    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }
    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closetTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach(Enemy child in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, child.transform.position);
            if(targetDistance < maxDistance)
            {
                closetTarget = child.transform;
                maxDistance = targetDistance;
            }
        }
        target = closetTarget;
    }
    void AimWeapon()
    {
        float  targetDistance = Vector3.Distance(transform.position, target.position); // check enemy distance
        transform.LookAt(target);
        if(targetDistance < range) // attack if on range
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }
    void Attack(bool isActive)
    {
        var emissionModule = projectileParticles.emission;
        emissionModule.enabled = isActive;
    }
}
