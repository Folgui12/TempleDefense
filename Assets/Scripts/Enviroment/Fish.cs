using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform[] wayPoints;
    private int wayPointIndex;
    // Update is called once per frame
    void FixedUpdate()
    {
        FollowWayPoints();
    }
    void FollowWayPoints()
    {
        Vector3 dir = wayPoints[wayPointIndex].transform.position - transform.position;
        GoToWayPoint(dir);
    }
    void IncreaseWaypointIndex()
    {
        wayPointIndex++;
        if (wayPointIndex >= wayPoints.Length)
        {
            wayPointIndex = 0;
        }
    }
    void GoToWayPoint(Vector3 dir)
    {
        transform.position += dir * Time.deltaTime * speed;
        if (dir.x == 0 && dir.z == 0) return;
        transform.forward = dir - new Vector3(0, -90, 0);
        if (Vector3.Distance(transform.position, wayPoints[wayPointIndex].transform.position) < 40f)
        {
            IncreaseWaypointIndex();
        }
    }
}
