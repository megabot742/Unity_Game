using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField] float lightDecay = 0.1f;
    [SerializeField] float angleDecay = 1f;
    [SerializeField] float minimumAnale = 10f;

    Light myLight;

    void Start() 
    {
        myLight = GetComponent<Light>();     
    }
    void Update()
    {
        DecreaseLightAngle();
        DecreaseLightIntensity();
    }
    public void RestoreLightAngle(float restoreAngle)
    {
        myLight.spotAngle += restoreAngle;
    }
    public void AddLightIntensity(float intensityAmount)
    {
        myLight.intensity += intensityAmount; 
    }
    void DecreaseLightAngle()
    {
        if(myLight.spotAngle > minimumAnale)
        {
            myLight.spotAngle -= angleDecay * Time.deltaTime;
            myLight.innerSpotAngle -= angleDecay * Time.deltaTime;
        }
        else
        {
            return;
        }
    }
    void DecreaseLightIntensity()
    {
        myLight.intensity -= lightDecay * Time.deltaTime; 
    }
}
