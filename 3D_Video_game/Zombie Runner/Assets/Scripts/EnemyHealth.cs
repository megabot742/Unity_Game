using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoint = 100f;
    float deplayDie = 5f;
    bool isDead = false;
    public bool IsDead()
    {
        return isDead;
    }
    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");
        hitPoint -= damage;
        if(hitPoint < Mathf.Epsilon)
        {
            Die();
        }
    }
    void Die()
    {
        if(hitPoint <= Mathf.Epsilon && !isDead) //check zombie die or not
        {
            isDead = true; //else
            GetComponent<Animator>().SetTrigger("die");
            Destroy(gameObject, deplayDie);
        }
    }
}
