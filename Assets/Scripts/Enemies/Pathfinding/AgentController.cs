using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    public BaseEnemyController enemy;
    public float radius = 3;
    public LayerMask maskNodes;
    public LayerMask maskObs;
    public Nodes target;
    public Box box;
    public void RunBFS()
    {
        var start = GetNearNode(enemy.transform.position);
        if (start == null) return;
        Debug.Log("BFS?");
        List<Nodes> path = BFS.Run(start, GetConnections, IsSatiesfies);
        enemy.GetStateWaypoints.SetWayPoints(path);
        box.SetWayPoints(path);
    }
    Nodes GetNearNode(Vector3 pos)
    {
        var nodes = Physics.OverlapSphere(pos, radius, maskNodes);
        Nodes nearNode = null;
        float nearDistance = 0;
        for (int i = 0; i < nodes.Length; i++)
        {
            var currentNode = nodes[i];
            var dir = currentNode.transform.position - pos;
            float currentDistance = dir.magnitude;
            if (nearNode == null || currentDistance < nearDistance)
            {
                if (!Physics.Raycast(pos, dir.normalized, currentDistance, maskObs))
                {
                    nearNode = currentNode.GetComponent<Nodes>();
                    nearDistance = currentDistance;
                }
            }
        }
        return nearNode;
    }
    List<Nodes> GetConnections(Nodes current)
    {
        return current.neightbourds;
    }
    bool IsSatiesfies(Nodes current)
    {
        return current == target;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(enemy.transform.position, radius);
    }
}
