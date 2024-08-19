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

    [SerializeField] private GameObject[] enemyColliderList;
    //public List<GameObject> enemyQueue = new();

    // Start is called before the first frame update
    void Start()
    {
        CurrentLife = _stats.Life;
        _los = GetComponent<LoS>();
    }
    
    public GameObject CheckClosestEnemy()
    {
        _currentEnemy = null;

        float shortestDistance = Mathf.Infinity;

        GameObject nearestEnemy = null;

        /*int cantColliders = Physics.OverlapSphereNonAlloc(transform.position, _stats.AttackRange, enemyColliderList, _layerMask);
        Debug.Log(cantColliders);

        if (cantColliders == 1 && collider.gameObject != enemyColliderList[0].gameObject)
        {
            enemyQueue.Add(enemyColliderList[0].gameObject);
            collider = enemyColliderList[0];
            if (_los.CheckRange(enemyColliderList[i].transform, _stats.AttackRange) && (enemyColliderList[i].CompareTag("Enemy") || enemyColliderList[i].CompareTag("golem")))
            {
                _currentEnemy = enemyColliderList[i].gameObject;
            }
        }*/

        enemyColliderList = ActiveEnemiesManager.Instance.activeEnemies;


        for (int i = 0; i < ActiveEnemiesManager.Instance.activeEnemies.Length; i++)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemyColliderList[i].transform.position);
            if (distanceToEnemy < shortestDistance && enemyColliderList[i].activeInHierarchy)
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
