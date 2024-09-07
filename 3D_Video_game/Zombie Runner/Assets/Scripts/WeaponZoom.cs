using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField]  GameObject fpsCamera;
    [SerializeField] float zoomedOutPOV = 40f;
    [SerializeField] float zoomedInPOV = 20f;
   
    bool zoomedInToggle = false;
     void Update() 
    {
        if(Input.GetMouseButtonDown(1))
        {
            if(!zoomedInToggle)
            {
                zoomedInToggle = true;
                fpsCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = zoomedInPOV;
            }
            else
            {
                zoomedInToggle = false;
                fpsCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = zoomedOutPOV;
                
            }
        }
    }
}
