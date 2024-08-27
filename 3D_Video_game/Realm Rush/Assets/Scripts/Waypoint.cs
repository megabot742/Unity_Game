using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceble = false;
    public bool GetIsPlacable()
    {
        return isPlaceble;
    }

    void OnMouseDown() 
    {
        if(isPlaceble) // check true
        {
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position); // == Instantiate(towerPrefab, transform.position , Quaternion.identity); 
            isPlaceble = !isPlaced;
        }
    }
}
