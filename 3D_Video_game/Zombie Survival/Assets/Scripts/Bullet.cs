using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision objectHit) {
        if(objectHit.gameObject.CompareTag("Target"))
        {
            print("Hit " + objectHit.gameObject.name + "!");
            CreateBulletImpactEffect(objectHit);
            Destroy(gameObject);
        }    
        if(objectHit.gameObject.CompareTag("Wall"))
        {
            print("Hit a wall");
            CreateBulletImpactEffect(objectHit);
            Destroy(gameObject);
        }
    }
    void CreateBulletImpactEffect(Collision objectHit)
    {
        ContactPoint contact = objectHit.contacts[0];

        GameObject hole = Instantiate(GlobalReferences.Instance.bulletImpactEffectPrefab,contact.point,Quaternion.LookRotation(contact.normal));
        
        hole.transform.SetParent(objectHit.gameObject.transform);
    }
}
