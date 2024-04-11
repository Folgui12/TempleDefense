using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyModel : MonoBehaviour
{
    public GameObject _mainBuilding;

    public GameObject _currentBuilding;

    //public float speed;
    //public float attackRange;

    [SerializeField] private EnemyStats _stats;

    Rigidbody _rb;

    LoS lineOfSight;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _mainBuilding = GameObject.Find("MainBuilding");
        lineOfSight = GetComponent<LoS>();
        _currentBuilding = _mainBuilding;
    }

    public void Move(Vector3 dir)
    {
        dir *= _stats.travelSpeed;
        dir.y = _rb.velocity.y;
        _rb.velocity = dir;
    }

    public void GoToMainBuilding()
    {
        Move((_currentBuilding.transform.position - transform.position).normalized);
    }

    public void Attack()
    {
        Debug.Log("Atancando");
    }

    public bool CheckDistance()
    {
        if (Vector3.Distance(transform.position, _currentBuilding.transform.position) < _stats.attackRange)
            return true;
        else
            return false;
    }

    public bool CheckYPosition()
    {
        return transform.position.y <1.5? true: false;
    }

    public void CheckClosest()
    {
        Collider[] colliderList = Physics.OverlapSphere(transform.position, lineOfSight.range);

        for(int i = 0; i < colliderList.Length; i++)
        {
            if (colliderList[i].tag == "Mine" && lineOfSight.CheckRange(colliderList[i].transform))
            {
                Debug.Log(colliderList[i].gameObject);
                _currentBuilding = colliderList[i].gameObject;
            }
        }
        
    }

}
