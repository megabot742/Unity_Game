using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        Debug.Log(startingPosition);
    }

    // Update is called once per frame
    void Update() 
    {
        if(period <= Mathf.Epsilon) {return;}
        float cycles = Time.time/ period; //continuall growing over  time (chu kì)

        const float tau = Mathf.PI * 2; // constant value of 6.283 - hằng số Tau (6.18....)
        float rawSinWave = Mathf.Sin(cycles * tau); // range -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f; // change range -1 to 1 -> range 0 to 2 -> then /2f to range 0 to 1 

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
