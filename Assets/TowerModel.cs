using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerModel : MonoBehaviour, IDamageable
{
    public DefenseStats _stats;

    public GameObject _currentEnemy;

    public float CurrentLife;
    LoS _los;
    
    // Start is called before the first frame update
    void Start()
    {
        CurrentLife = _stats.Life;
        _los = GetComponent<LoS>();
    }
    
    public GameObject CheckClosestEnemy()
    {
        _currentEnemy = null;

        Collider[] colliderList = Physics.OverlapSphere(transform.position, _stats.AttackRange);

        for(int i = 0; i < colliderList.Length; i++)
        {
            if (colliderList[i].tag == "Enemy" && _los.CheckRange(colliderList[i].transform, _stats.AttackRange))
            {
                _currentEnemy = colliderList[i].gameObject;
            }
        }

        return _currentEnemy;
    }
    public void Dead()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            BulletMovement arrowHit = other.gameObject.GetComponent<BulletMovement>();

            TakeDamage(arrowHit.Damage);
        }

        if(other.gameObject.CompareTag("Enemy"))
        {
            BaseEnemyModel enemyRef = other.gameObject.GetComponent<MeleeDamageRef>().EnemyModel;

            TakeDamage(enemyRef._stats.Damage);
        }
    }

    public void TakeDamage(int damage)
    {
        CurrentLife -= damage;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;      
        Gizmos.DrawWireSphere(transform.position, _stats.AttackRange);
    }
}
