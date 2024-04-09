using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyModel : MonoBehaviour
{
    private GameObject _mainBuilding;

    private GameObject _newBuilding;
    private GameObject _currentBuilding;

    public float speed;

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
        dir *= speed;
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
        if (Vector3.Distance(transform.position, _currentBuilding.transform.position) < 3)
            return true;
        else
            return false;
    }

    public bool checkYPosition()
    {
        return transform.position.y <1.5? true: false;
    }

    public void checkClosest()
    {
        Collider[] colliderList = Physics.OverlapSphere(transform.position, lineOfSight.range);

        foreach (Collider item in colliderList)
        {
            if(lineOfSight.CheckRange(item.transform))
            {

            }
        }
        
    }

}
