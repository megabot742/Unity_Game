using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f,5f)]float speed;
    void Start()
    {
        StartCoroutine(FollowPath());
    }
    IEnumerator FollowPath()
    {
        foreach(Waypoint waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float travelPercent = 0f;

            transform.LookAt(endPosition); //enmy face the direction of travel

            while(travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;//control enemy speed
                transform.position = Vector3.Lerp(startPosition,endPosition,travelPercent);//enemy smoothly between waypoint
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
