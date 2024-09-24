using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseEnemyModel : MonoBehaviour, IDamageable, IBoid
{
    public GameObject _mainBuilding;

    public GameObject _currentBuilding;

    public float CurrentLife;

    public bool OnHand = false;

    public bool OnGround = true;

    [SerializeField] public EnemyStats _stats;

    private Rigidbody _rb;

    [SerializeField] Collider[] rigColliders;

    [SerializeField] Rigidbody[] rigColliders2;

    public Collider _collider;

    private LoS lineOfSight;

    private BaseEnemyView _view;

    public AgentController _agentController;

    public LeaderBehaviour _leaderBehaviour;

    public WaveSpawner _waveSpawner;

    public AudioSource audioSource;

    public GameObject _Body;
    public GameObject _Armature;
    public int enemyType;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _mainBuilding = GameObject.Find("Templo");
        lineOfSight = GetComponent<LoS>();
        _view = GetComponent<BaseEnemyView>();
        _currentBuilding = _mainBuilding;
        _waveSpawner = FindObjectOfType<WaveSpawner>();
        audioSource = GetComponent<AudioSource>();
        rigColliders = GetComponentsInChildren<Collider>();
        rigColliders2 = GetComponentsInChildren<Rigidbody>();

        CurrentLife = _stats.life;
    }
    private void Start()
    {
        _agentController.temple = _mainBuilding;
        foreach (Collider col in rigColliders)
        {
            col.enabled = false;
        }
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
        CurrentLife -= damage;
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

            case 3:
                AudioManager.Instance.Play("ArrowHit", audioSource);        // Harpy
                break;
        }
    }

    public void Dead()
    {
        foreach (Collider col in rigColliders)
        {
            col.enabled = !col.enabled!;
        }
        foreach (Rigidbody col in rigColliders2)
        {
            col.useGravity = false;
        }
        _collider.enabled = false;
        _view.anim.enabled = false;

        Debug.Log(_view.col);
        _view.col.enabled = false;

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

            case 3:
                AudioManager.Instance.Play("HighPop", audioSource);         // Harpy
                break;
        }

        Invoke("KickModel", 3);
    }

    private void KickModel()
    {
        if (transform.parent.gameObject.active)
        {
            CurrencyManager.Instance.AddMoney(_stats.moneyQuantity);
            _waveSpawner.RemoveEnemy(this.transform.parent.gameObject);
        }

    }

    public void EnemyOnHand()
    {
        OnHand = true;
        OnGround = false;
        _rb.velocity = new Vector3(0,0,0);
    //foreach (Rigidbody col in rigColliders2)
    //{
    //    col.useGravity = false;
    //}
        switch (enemyType)
        {
            case 0:
                AudioManager.Instance.Play("CentaurGrabbed", audioSource);      // Centaur
                break;

            case 1:
                AudioManager.Instance.Play("SatyrGrabbed", audioSource);        // Satyr
                break;

            case 2:
                //AudioManager.Instance.Play("GolemHit", audioSource);          // Golem
                break;

            case 3:
                AudioManager.Instance.Play("HarpyGrabbed", audioSource);        // Harpy
                break;
        }
    }

    public void EnemyOffHand()
    {
        OnHand = false;
        //foreach (Rigidbody col in rigColliders2)
        //{
        //    col.useGravity = true;
        //}
    }
    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public void RagdollOn()
    {
        foreach (Collider col in rigColliders)
        {
            col.enabled = !col.enabled!;
        }
        _collider.enabled = true;
        _view.anim.enabled = false;
    }
    public void RagdollOff() 
    {
        foreach (Collider col in rigColliders)
        {
            col.enabled = !col.enabled!;
        }
        _collider.enabled = true;
        _view.anim.enabled = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            OnGround = true;
        }
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
