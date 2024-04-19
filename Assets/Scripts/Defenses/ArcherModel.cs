using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class ArcherModel : MonoBehaviour
{
    [SerializeField] private GameObject _arrow;
    [SerializeField] private Transform _arrowSpawnPoint;
    private TowerModel _tModel;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        _tModel = GetComponentInParent<TowerModel>();
        anim = GetComponent<Animator>();

        ArcherEventManager.ShootEvent += StartShootAnimation;
    }

    private void StartShootAnimation()
    {
        Debug.Log("Disparando");
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0))
            anim.SetTrigger("Shoot");
    }

    private void Shoot()
    {
        BulletMovement arrow = Instantiate(_arrow, _arrowSpawnPoint.position, Quaternion.Euler(new Vector3(0, 0, 90))).GetComponent<BulletMovement>();

        arrow.enemyTarget = _tModel._currentEnemy;
    }
    
}
