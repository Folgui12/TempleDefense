using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerModel : MonoBehaviour, IDamageable
{
    public DefenseStats _stats;

    public GameObject _currentEnemy;

    public float CurrentLife;
    LoS _los;

    [SerializeField] GridCollider _grid;

    public LayerMask _layerMask;


    // Start is called before the first frame update
    void Start()
    {
        CurrentLife = _stats.Life;
        _los = GetComponent<LoS>();
        AudioManager.Instance.Play("ShootArrow");

    }

    public GameObject CheckClosestEnemy()
    {
        GameObject[] enemyColliderList;

        _currentEnemy = null;

        float shortestDistance = Mathf.Infinity;

        GameObject nearestEnemy = null;

        enemyColliderList = ActiveEnemiesManager.Instance.activeEnemies;


        for (int i = 0; i < ActiveEnemiesManager.Instance.activeEnemies.Length; i++)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemyColliderList[i].transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemyColliderList[i];
            }
        }
        
        if (nearestEnemy != null && _los.CheckRange(nearestEnemy.transform, _stats.AttackRange))
        {
            _currentEnemy = nearestEnemy;
        }


        

        return _currentEnemy;

        //GameObject nearestEnemy = null;

        /*if (nearestEnemy != null && _los.CheckRange(nearestEnemy.transform, _stats.AttackRange))
        {
            _currentEnemy = nearestEnemy;
        }*/

        //List<GameObject> currentEnemies = WaveSpawner.Instance.spawnedEnemies;

        

    }
    public void Dead()
    {
        _grid.KillCollider();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            BulletMovement arrowHit = other.gameObject.GetComponent<BulletMovement>();

            TakeDamage(arrowHit.Damage);
        }

        if(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("golem"))
        {
            BaseEnemyModel enemyRef = other.gameObject.GetComponent<MeleeDamageRef>().EnemyModel;

            TakeDamage(enemyRef._stats.Damage);
        }
    }

    public virtual void TakeDamage(int damage)
    {
        CurrentLife -= damage;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;      
        Gizmos.DrawWireSphere(transform.position, _stats.AttackRange);
    }
}
