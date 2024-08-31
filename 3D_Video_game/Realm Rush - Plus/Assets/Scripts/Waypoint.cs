using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceble = false;
    GridManager gridManager;
    Pathfinder pathfinder;
    Vector2Int coordinates = new Vector2Int();
    public bool GetIsPlacable()
    {
        return isPlaceble;
    }
    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    void Start()
    {
        if(gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if(!isPlaceble)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }
    void OnMouseDown() 
    {
        if(gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates)) // check GetNode && WillBlockPath
        {
            bool isSuccessful = towerPrefab.CreateTower(towerPrefab, transform.position); // == Instantiate(towerPrefab, transform.position , Quaternion.identity); 
            if(isSuccessful)
            {
                gridManager.BlockNode(coordinates);
                pathfinder.NotiReceivers();
            }
        }
    }
}
