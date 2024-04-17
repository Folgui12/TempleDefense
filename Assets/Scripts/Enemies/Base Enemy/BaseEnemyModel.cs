using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseEnemyModel : MonoBehaviour
{
    public GameObject _mainBuilding;

    public GameObject _currentBuilding;

    public bool isGround;

    [SerializeField] public float _life;

    [SerializeField] private EnemyStats _stats;

    Rigidbody _rb;

    LoS lineOfSight;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _mainBuilding = GameObject.Find("MainBuilding");
        lineOfSight = GetComponent<LoS>();
        _currentBuilding = _mainBuilding;
        isGround = false;
    }

    public void Move(Vector3 dir)
    {
        dir *= _stats.travelSpeed;
        dir.y = _rb.velocity.y;
        _rb.velocity = dir;
    }

    public void LookDir(Vector3 dir)
    {
        if (dir.x == 0 && dir.z == 0) return;

        transform.forward = dir;
    }

    public void Attack()
    {
        Debug.Log("Atancando");
    }

    public GameObject CheckClosest()
    {
        Collider[] colliderList = Physics.OverlapSphere(transform.position, lineOfSight.range);

        for(int i = 0; i < colliderList.Length; i++)
        {
            if (colliderList[i].tag == "Mine" && lineOfSight.CheckRange(colliderList[i].transform))
            {
                _currentBuilding = colliderList[i].gameObject;
            }
            else if (Vector3.Distance(_currentBuilding.transform.position , transform.position) > lineOfSight.range)
            {
                _currentBuilding = _mainBuilding;
            }
        }
        return _currentBuilding;
    }

    public void Dead()
    {
        Destroy(gameObject);
    }

    public float Life => _life;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isGround = true;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isGround = false;
        }
    }

}
