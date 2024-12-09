using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerModel : MonoBehaviour, IDamageable
{
    public DefenseStats _stats;

    public GameObject _currentEnemy;

    public AudioSource audioSource;

    public float CurrentLife;
    LoS _los;

    [SerializeField] GridCollider _grid;

    public LayerMask _layerMask;

    [SerializeField] private LineRenderer circle;
    [SerializeField] private float TimeBeforeCircleAppears;

    public Material _material;
    public Material _dissolve;
    public bool finishDelay;
    public float delayTime = 0;
    [SerializeField] private float delay;
    [SerializeField] private MeshRenderer _mesh;

    // Start is called before the first frame update
    void Start()
    {
        _mesh = GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();
        _los = GetComponent<LoS>();

        AudioManager.Instance.Play("Construction", audioSource);

        CurrentLife = _stats.Life;
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
    }

    public void DelayAnim()
    {
        delayTime += Time.deltaTime / delay;
        _dissolve.SetFloat("_Speed", delayTime);
    }
    public void ChangeMaterial()
    {
        _mesh.material = _material;
    }

    public void Dead()
    {
        _grid.KillCollider();
    }

    public void UpdateCircle()
    {
        DrawCircle(100, _stats.AttackRange);
    }

    void DrawCircle(int steps, float radius)
    {
        circle.enabled = true;
        circle.positionCount = steps;

        for (int currentStep = 0; currentStep < steps; currentStep++)
        {
            float circumferenceProgress = (float)currentStep / steps;

            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            float x = xScaled * radius;
            float y = yScaled * radius;

            Vector3 currentPosition = new Vector3(transform.position.x + x, 0, transform.position.z + y);

            circle.SetPosition(currentStep, currentPosition);
        }
    }
    public void StopDrawingCircles()
    {
        circle.enabled = false;
    }
    private void OnDestroy()
    {
        if(name != "Muro")
        {
            SpawnDefenseArea spawnRef = GetComponentInParent<SpawnDefenseArea>();
            if(spawnRef != null ) 
                spawnRef.CanSpawnAgain();
        }    
                
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyArrow"))
        {
            BulletMovement arrowHit = other.gameObject.GetComponent<BulletMovement>();

            TakeDamage(arrowHit.Damage);
        }

        if(other.gameObject.CompareTag("Enemy") || other.gameObject.layer == 17)
        {
            BaseEnemyModel enemyRef = other.gameObject.GetComponent<MeleeDamageRef>().EnemyModel;

            TakeDamage(enemyRef._stats.Damage);
        }
    }

    public virtual void TakeDamage(int damage)
    {
        AudioManager.Instance.Play("WoodHit", audioSource);
        CurrentLife -= damage;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;      
        Gizmos.DrawWireSphere(transform.position, _stats.AttackRange);
    }
}
