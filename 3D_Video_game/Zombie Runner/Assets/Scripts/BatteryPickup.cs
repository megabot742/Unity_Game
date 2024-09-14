using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    [SerializeField] float restoreAngle = 40f;
    [SerializeField] float addIntensity = 1;
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player")
        {
            FindObjectOfType<FlashLight>().RestoreLightAngle(restoreAngle);
            FindObjectOfType<FlashLight>().AddLightIntensity(addIntensity);
            Debug.Log("battery");
            Destroy(gameObject);
        }
    }
}
