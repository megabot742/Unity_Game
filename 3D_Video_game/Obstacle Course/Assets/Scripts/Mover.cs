using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        PrintInstructuon();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }
    void PrintInstructuon()
    {
        Debug.Log("Welcome to the game");
        Debug.Log("Move your player with WASD or Arrow keys");
        Debug.Log("Don't hit the wall");
    }
    void MovePlayer()
    {
        float xValue = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float zValue = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        transform.Translate(xValue,0,zValue);
    }
}
