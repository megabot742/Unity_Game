using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
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
        GameObject parent = GameObject.FindGameObjectWithTag("Path"); //find GameObjects with tag "Path"
        foreach(Transform child in parent.transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>(); 
            if(waypoint != null)
            {
                path.Add(waypoint); // add GameObject after find to list
            }
        }
    }
    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }
    void FinishPath()
    {
        enemy.StealdGold();
        gameObject.SetActive(false);
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
        FinishPath();
          
    }
}
