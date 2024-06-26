using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public interface IPoints
{
    void SetWayPoints(List<Node> newPoints);
    void SetWayPoints(List<Vector3> newPoints);
}
