using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoint = 100f;
    public void playerTakeDamage(float damage)
    {
        hitPoint -= damage;
        if(hitPoint < Mathf.Epsilon)
        {
            GetComponent<DeathHandler>().HandleDeath();
        }
    }
}
