using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
   [SerializeField] int health = 50;

   void OnTriggerEnter2D(Collider2D other) 
   {
        DamgeDealer damgeDealer = other.GetComponent<DamgeDealer>();
        if(damgeDealer !=null)
        {
            TakeDamage(damgeDealer.GetDamage());
            damgeDealer.Hit();
        }
   }
    void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
