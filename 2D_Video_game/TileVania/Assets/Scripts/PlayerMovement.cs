using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    Vector2 moveInput;
    Rigidbody2D myrigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        myrigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
    }
    void OnMove(InputValue value)
    {   
        moveInput = value.Get<Vector2>();
        // Debug.Log(moveInput);
    }
    void Run()
    {
        Vector2 playVelocity = new Vector2(moveInput.x * runSpeed, myrigidbody2D.velocity.y);
        myrigidbody2D.velocity = playVelocity;
    }
    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myrigidbody2D.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myrigidbody2D.velocity.x), 1f);
        }
    }
}
