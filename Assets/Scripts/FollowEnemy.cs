using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    [SerializeField] private Transform enemyPosition;
    [SerializeField] private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(new Vector3(-45f, 0f, 0f)); 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = enemyPosition.position + offset;
    }
}
