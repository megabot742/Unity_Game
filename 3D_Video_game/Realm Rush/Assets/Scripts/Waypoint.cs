using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] GameObject towerPrefab;
    [SerializeField] bool isPlaceble = false;
   void OnMouseDown() 
   {
    if(isPlaceble)
    {
        Instantiate(towerPrefab, transform.position , Quaternion.identity);
        isPlaceble = false;
    }
   }
}
