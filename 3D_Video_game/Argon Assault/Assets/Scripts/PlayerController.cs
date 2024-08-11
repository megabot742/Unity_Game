using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General Setup Setting")]
    [Tooltip("How fast ship moves up and down based upon player input")]
    [SerializeField] float speedControl= 30f;
    [Tooltip("How far player moves horizontally")][SerializeField] float xRange = 10f;
    [Tooltip("How far player moves vertically")][SerializeField] float yRange = 7f;

    [Header("Laser gun array")]
    [Tooltip("Add all player lasers here")]
    [SerializeField] GameObject[] lasers;

    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 2f;

    [Header("Player input based tuning")]
    [SerializeField] float controlPicthFactor = -15f;
    [SerializeField] float controlRollFactor = -15f;
    float xThrow,yThrow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessTranslation()
    {
        //PLayer Input
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");
        //Turn right and left
        float xOffset = speedControl * xThrow * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange); //limited range
        //Turn up and down
        float yOffset = speedControl * yThrow * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange); //limited range

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
    void ProcessRotation()
    {
        //Pitch
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = controlPicthFactor * yThrow;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        //Yaw
        float yaw = transform.localPosition.x * positionYawFactor ;

        //Roll
        float roll = controlRollFactor * xThrow; 
        transform.localRotation = Quaternion.Euler(pitch,yaw,roll);
    }
    void ProcessFiring()
    {
        if(Input.GetKey(KeyCode.Space)) //press space for shooting
        {
            SetActiveLasers(true);
        }
        else
        {
            SetActiveLasers(false);
        }
    }
    void SetActiveLasers(bool isActive)
    {
        foreach (GameObject iteam in lasers)
        {
           var emissionModule = iteam.GetComponent<ParticleSystem>().emission;
           emissionModule.enabled = isActive;
        }
    }
}
