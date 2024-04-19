using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class ArcherModel : MonoBehaviour
{
    [SerializeField] private GameObject _arrow;
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
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0))
            anim.SetTrigger("Shoot");
    }

    private void Shoot()
    {
        BulletMovement arrow = Instantiate(_arrow, transform.position, transform.rotation).GetComponent<BulletMovement>();

        arrow.enemyTarget = _tModel._currentEnemy;
    }
    
}
