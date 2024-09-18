using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class ArcherModel : MonoBehaviour
{
    [SerializeField] private GameObject _arrow;
    [SerializeField] private Transform _arrowSpawnPoint;
    [SerializeField] private ObjectPoolTowerArrow _poolArrows;
    private TowerModel _tModel;
    public Animator anim;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _tModel = GetComponentInParent<TowerModel>();
        _poolArrows = GameObject.FindObjectOfType<ObjectPoolTowerArrow>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();


        //ArcherEventManager.ShootEvent += StartShootAnimation;
    }

    public void StartShootAnimation()
    {
        if (anim != null && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0))
            anim.SetTrigger("Shoot");

    }

    private void Shoot()
    {
        //BulletMovement arrow = Instantiate(_arrow, _arrowSpawnPoint.position, Quaternion.Euler(new Vector3(0, 0, 90))).GetComponent<BulletMovement>();
        //arrow.Target = _tModel._currentEnemy;
        //arrow.Damage = _tModel._stats.Damage;
        AudioManager.Instance.Play("ShootArrow", audioSource);
        GameObject currentArrow = _poolArrows.GetPooled(_arrowSpawnPoint, _arrow, Quaternion.Euler(new Vector3(0, 0, 90)));
        BulletMovement arrowRef = currentArrow.GetComponent<BulletMovement>();

        arrowRef.Target = _tModel._currentEnemy;
        arrowRef.Damage = _tModel._stats.Damage;

    }

}
