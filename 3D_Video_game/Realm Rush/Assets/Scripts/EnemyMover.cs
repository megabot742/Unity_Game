using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f,5f)]float speed;
    Enemy enemy;
    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }
    void Start()
    {
        enemy = GetComponent<Enemy>();
    }
    void FindPath()
    {
        path.Clear();
        GameObject[] parent = GameObject.FindGameObjectsWithTag("Path"); //find GameObjects with tag "Path"
        foreach(GameObject child in parent)
        {
            path.Add(child.GetComponent<Waypoint>()); // add GameObject after find to list
        }
    }
    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
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
        enemy.StealdGold();
        gameObject.SetActive(false);  
    }
}
