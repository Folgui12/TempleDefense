using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoS 
{
     public bool CheckRange(Transform target);
     public bool CheckAngle(Transform target);
     public bool CheckObstacle(Transform target);
     public Vector3 Origin();
     public Vector3 Forward();
}
