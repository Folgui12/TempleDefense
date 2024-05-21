using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public GameObject Target;

    public float arrowSpeed;
    
    // Update is called once per frame
    void Update()
    {
        if (Target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position + new Vector3(0, 1, 0), arrowSpeed * Time.deltaTime);
            transform.LookAt(Target.transform.position + new Vector3(0, 1, 0), Vector3.up);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Building")
        {
            Destroy(gameObject);
        }
    }
}
