using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletMovement : ManagedUpdateBehavior
{
    public GameObject Target;

    public GameObject LockOnTarget;

    public float arrowSpeed;

    public int Damage;

    public float lifeTime;

    public float lifeCounter;

    public ObjectPoolArrows arrowArrows;
    public ObjectPoolTowerArrow arrowTower;

    private void Awake()
    {
        if(gameObject.tag == "EnemyArrow")
        {
            arrowArrows = GameObject.FindObjectOfType<ObjectPoolArrows>();
        }
        else
        {
            arrowTower = GameObject.FindObjectOfType<ObjectPoolTowerArrow>();
        }
    }
    private void OnEnable()
    {
        lifeCounter = 0;
    }
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
            if (gameObject.tag == "EnemyArrow")
            {
                arrowArrows.ReturnToPool(gameObject);
            }
            else
            {
                arrowTower.ReturnToPool(gameObject);
            }
        }

        if (Target != null && Target.activeInHierarchy)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position + new Vector3(0, 1, 0), arrowSpeed * Time.deltaTime);
            transform.LookAt(Target.transform.position + new Vector3(0, 1, 0), Vector3.up);
        }
        else
        {
            if (gameObject.tag == "EnemyArrow")
            {
                arrowArrows.ReturnToPool(gameObject);
            }
            else
            {
                arrowTower.ReturnToPool(gameObject);
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GenericEnemy") || other.gameObject.CompareTag("Building"))
        {
            if (gameObject.tag == "EnemyArrow")
            {
                arrowArrows.ReturnToPool(gameObject);
            }
            else
            {
                arrowTower.ReturnToPool(gameObject);
            }
        }
    }
}
