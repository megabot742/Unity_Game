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
    void OnDisable() 
    {
        ZoomOut(); // zoom out camera when change another Weapon 
    }
    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Mouse1)) //click right mouse
        {
            if(!zoomedInToggle)
            {
                ZoomIn();
            }
            else
            {
                ZoomOut();
            }
        }
    }
    void ZoomIn()
    {
        zoomedInToggle = true;
        fpsCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = zoomedInPOV;
    }
    void ZoomOut()
    {
        zoomedInToggle = false;
        fpsCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = zoomedOutPOV;
    }
}
