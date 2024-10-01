using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private ObjectPoolArrows _poolArrows;
    private BaseEnemyModel _enemyModel;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        _enemyModel = GetComponent<BaseEnemyModel>();
        _poolArrows = FindObjectOfType<ObjectPoolArrows>();
        anim = GetComponent<Animator>();

        EnemyEventManager.ShootEvent += StartShootAnimation;
    }

    private void StartShootAnimation()
    {
        if (anim != null && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0))
            anim.SetTrigger("Attack");
    }

    private void Shoot()
    {
        //BulletMovement arrow = Instantiate(_projectile, _shootPoint.position, Quaternion.Euler(new Vector3(0, 0, 90))).GetComponent<BulletMovement>();
        GameObject currentArrow = _poolArrows.GetPooled(_shootPoint, _projectile, Quaternion.Euler(new Vector3(0, 0, 90)));
        //arrow.Target = _enemyModel._currentBuilding;
        //arrow.Damage = _enemyModel._stats.Damage;

        BulletMovement arrowRef = currentArrow.GetComponent<BulletMovement>();

        arrowRef.Target = _enemyModel._currentBuilding;
        arrowRef.Damage = _enemyModel._stats.Damage;
    }
}
