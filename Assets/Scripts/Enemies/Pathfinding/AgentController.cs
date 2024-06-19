using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    public BaseEnemyController _enemy;
    public float radius = 3;
    public LayerMask maskNodes;
    public LayerMask maskObs;
    public MainBuildingManager temple;
    public MyGrid myGrid;
    public void RunAStarPlusVector()
    {
        Vector3 start = myGrid.GetPosInGrid(_enemy.transform.position);
        List<Vector3> path = AStar.Run(start, GetConnections, IsSatiesfies, GetCost, Heuristic, 5000);
        //path = AStar.CleanPath(path, InView);
        //_enemy.GetStateWaypoints.SetWayPoints(path);
        //temple.SetWayPoints(path);
    }
    float Heuristic(Vector3 current)
    {
        float heuristic = 0;
        float multiplierDistance = 1;
        heuristic += Vector3.Distance(current, temple.transform.position) * multiplierDistance;
        return heuristic;
    }
    float GetCost(Vector3 parent, Vector3 child)
    {
        float cost = 0;
        float multiplierDistance = 1;
        cost += Vector3.Distance(parent, child) * multiplierDistance;
        return cost;
    }
    List<Vector3> GetConnections(Vector3 current)
    {
        var connections = new List<Vector3>();

        for (int x = -1; x <= 1; x++)
        {
            for (int z = -1; z <= 1; z++)
            {
                if (z == 0 && x == 0) continue;
                Vector3 point = myGrid.GetPosInGrid(new Vector3(current.x + x, current.y, current.z + z));
                Debug.Log(point + "  " + myGrid.IsRightPos(point));
                if (myGrid.IsRightPos(point))
                {
                    connections.Add(point);
                }
            }
        }
        return connections;
    }
    bool InView(Vector3 a, Vector3 b)
    {
        //a->b  b-a
        Vector3 dir = b - a;
        return !Physics.Raycast(a, dir.normalized, dir.magnitude, maskObs);
    }
    bool IsSatiesfies(Vector3 current)
    {
        return Vector3.Distance(current, temple.transform.position) < 2 && InView(current, temple.transform.position);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_enemy.transform.position, radius);
    }
}