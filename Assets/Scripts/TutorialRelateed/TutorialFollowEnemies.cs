using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFollowEnemies : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private float offset;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(enemy.transform.position.x, offset, enemy.transform.position.z);
    }
}
