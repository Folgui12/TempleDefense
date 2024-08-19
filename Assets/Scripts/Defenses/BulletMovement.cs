using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : ManagedUpdateBehavior
{
    public GameObject Target;

    public GameObject LockOnTarget;

    public float arrowSpeed;

    public int Damage;

    public float lifeTime;

    private float lifeCounter;

    
    // Update is called once per frame
    override protected void CustomLightUpdate()
    {
        base.CustomLightUpdate();

        if(lifeCounter < lifeTime)
        {
            lifeCounter += Time.deltaTime;
        }   
        else
        {
            Destroy(gameObject);
        }

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
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Building"))
        {
            Destroy(gameObject);
        }
    }
}
