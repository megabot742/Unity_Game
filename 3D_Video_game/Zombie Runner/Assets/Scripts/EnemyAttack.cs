using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    PlayerHealth target;
    [SerializeField] float damage = 20f;
    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
    }
    public void AttackHitEvent()
    {
        if (target == null) return;
        target.playerTakeDamage(damage);
        Debug.Log("Gra~~gra!!");
        target.GetComponent<DisplayDamage>().ShowDamageImpact();
    }
}
