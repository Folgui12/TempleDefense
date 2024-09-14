using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyPos : MonoBehaviour
{
    [SerializeField] private Transform FloorPos;
    void Update()
    {
        transform.position = new Vector3(FloorPos.position.x, transform.position.y, FloorPos.position.z);
    }
}
