using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseEnemyModel : MonoBehaviour, IDamageable, IBoid
{
    public GameObject _mainBuilding;

    public GameObject _currentBuilding;

    public float CurrentLife;

    private bool Tutorial;

    public bool OnHand = false;

    public bool OnGround = true;

    [SerializeField] public EnemyStats _stats;

    private Rigidbody _rb;

    private LoS lineOfSight;

    public BaseEnemyController _controller;

    public int enemyType;

    public AgentController _agentController;

    public LeaderBehaviour _leaderBehaviour;

    private WaveSpawner _waveSpawner;

    private TutorialSpawner _tutorialSpawner;

    public AudioSource audioSource;

    public bool Stuned;

    [SerializeField] private GameObject BloodVFX;
    [SerializeField] private GameObject BloodStainVFX;

    private bool once;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _mainBuilding = GameObject.Find("Templo");
        lineOfSight = GetComponent<LoS>();
        _currentBuilding = _mainBuilding;
        _tutorialSpawner = FindObjectOfType<TutorialSpawner>();
        _waveSpawner = FindObjectOfType<WaveSpawner>();
        audioSource = GetComponent<AudioSource>();
        CurrentLife = _stats.life;
        Stuned = false;
    }
    private void Start()
    {
        _agentController.temple = _mainBuilding;
    }
    private void OnEnable()
    {
        _rb.velocity = new Vector3(0, 0, 0);
        transform.position = _agentController.transform.position;
        CurrentLife = _stats.life;
        OnGround = false;
        once = true;
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

        if (_currentBuilding == null)
        {
            _currentBuilding = _mainBuilding;
        }
        for (int i = 0; i < colliderList.Length; i++)
        {
            if ((colliderList[i].gameObject.layer == 10 || colliderList[i].gameObject.layer == 9) && lineOfSight.CheckRange(colliderList[i].transform, _stats.viewRange))
            {
                _currentBuilding = colliderList[i].gameObject;
            }
            else if (Vector3.Distance(_currentBuilding.transform.position, transform.position) > _stats.viewRange)
            {
                _currentBuilding = _mainBuilding;
            }
        }
        return _currentBuilding;
    }

    public void TakeDamage(int damage)
    {
        switch (enemyType)
        {
            case 0:
                AudioManager.Instance.Play("ArrowHit", audioSource);        // Centaur
                break;

            case 1:
                AudioManager.Instance.Play("ArrowHit", audioSource);        // Satyr
                break;

            case 2:
                AudioManager.Instance.Play("GolemHit", audioSource);        // Golem
                break;
        }
        CurrentLife -= damage;
    }

    public void Dead()
    {
        if (transform.parent.gameObject.activeInHierarchy && once)
        {
            CurrencyManager.Instance.AddMoney(_stats.moneyQuantity);
            switch (enemyType)
            {
                case 0:
                    AudioManager.Instance.Play("LowPop", audioSource);          // Centaur
                    break;

                case 1:
                    AudioManager.Instance.Play("HighPop", audioSource);         // Satyr
                    break;

                case 2:
                    AudioManager.Instance.Play("GolemDeath", audioSource);      // Golem
                    break;
            }
            Instantiate(BloodVFX, new Vector3(transform.position.x, 2, 
                transform.position.z), transform.rotation);
            if (OnGround)
            {
                Instantiate(BloodStainVFX, new Vector3(transform.position.x, 0, transform.position.z),
                    transform.rotation);
            }
            if (!Tutorial) _waveSpawner.RemoveEnemy(this.transform.parent.gameObject);
            else _tutorialSpawner.RemoveEnemy(this.transform.parent.gameObject);
            once = false;
        }
    }

    public void EnemyOnHand()
    {
        OnHand = true;
        switch (enemyType)
        {
            case 0:
                AudioManager.Instance.Play("CentaurGrabbed", audioSource);      // Centaur
                break;

            case 1:
                AudioManager.Instance.Play("SatyrGrabbed", audioSource);        // Satyr
                break;

            case 2:
            //    AudioManager.Instance.Play("GolemHit", audioSource);          // Golem
                break;
        }
    }
    public void EnemyOffHand()
    {
        OnHand = false;
    }
    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }
    public void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Floor"))
        {
            OnGround = true;
        }
    }

    public void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Floor"))
        {
            OnGround = false;
        }
    }

    public Vector3 Position => transform.position;
    public Vector3 Front => transform.forward;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _stats.viewRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _stats.attackRange);
    }
}
