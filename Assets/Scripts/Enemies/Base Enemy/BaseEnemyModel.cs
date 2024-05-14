using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseEnemyModel : MonoBehaviour
{
    public GameObject _mainBuilding;

    public GameObject _currentBuilding;

    public bool isGround = true;

    public float CurrentLife;

    [SerializeField] public EnemyStats _stats;

    Rigidbody _rb;

    LoS lineOfSight;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _mainBuilding = GameObject.Find("MainBuilding");
        lineOfSight = GetComponent<LoS>();
        _currentBuilding = _mainBuilding;
        isGround = false;

        CurrentLife = _stats.life;
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

    public GameObject CheckClosest()
    {
        Collider[] colliderList = Physics.OverlapSphere(transform.position, _stats.viewRange);
        if(_currentBuilding == null)
        {
            _currentBuilding = _mainBuilding;
        }
        for (int i = 0; i < colliderList.Length; i++)
        {
            if ((colliderList[i].gameObject.layer == 10 || colliderList[i].gameObject.layer == 9) && lineOfSight.CheckRange(colliderList[i].transform, _stats.viewRange))
            {
                _currentBuilding = colliderList[i].gameObject;
            }
            else if (Vector3.Distance(_currentBuilding.transform.position , transform.position) > _stats.viewRange)
            {
                _currentBuilding = _mainBuilding;
            }
        }
        return _currentBuilding;
    }
    public void TakeDamage()
    {
        CurrentLife -= 1;
    }
    public void Dead()
    {
        CurrencyManager.Instance.AddMoney(_stats.moneyQuantity);
        Destroy(gameObject);
    }

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
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;      
        Gizmos.DrawWireSphere(transform.position, _stats.viewRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _stats.attackRange);
    }
}
