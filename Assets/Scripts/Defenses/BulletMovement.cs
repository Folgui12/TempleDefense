using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public GameObject enemyTarget;

    public float arrowSpeed;
    
    // Update is called once per frame
    void Update()
    {
        if (enemyTarget != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, enemyTarget.transform.position + new Vector3(0, 1, 0), arrowSpeed * Time.deltaTime);
            transform.LookAt(enemyTarget.transform.position + new Vector3(0, 1, 0), Vector3.up);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
