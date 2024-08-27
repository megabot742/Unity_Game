using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.black;
    TextMeshPro  label;
    Vector2Int coordinates = new Vector2Int();

    Waypoint waypoint;
    void Awake()
    {
        label = GetComponent<TextMeshPro>();
        // label.enabled = false;
        waypoint = GetComponentInParent<Waypoint>();
        DisplayCoordinates();
    }
    void Update()
    {
        if(!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }
        SetLabelColor();
        ToggleLabel();
    }
    void ToggleLabel() //check label
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }
    void SetLabelColor()
    {
        
        if(waypoint.GetIsPlacable() == true)
        {
            label.color = defaultColor;
        }
        else
        {
            label.color = blockedColor;
        }
    }
    void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);
        label.text = coordinates.x + "," + coordinates.y ;
    }
    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
