using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigibody;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;


    void Start()
    {
        myRigibody = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        myRigibody.velocity = new Vector2(moveSpeed, 0f);
    }
    void OnTriggerExit2D(Collider2D other) 
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }
    void FlipEnemyFacing()
    {   
        transform.localScale = new Vector2(-Mathf.Sign(myRigibody.velocity.x), 1f);
    }
}
