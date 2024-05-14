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
            transform.position = Vector3.MoveTowards(transform.position, enemyTarget.transform.position, arrowSpeed * Time.deltaTime);
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
