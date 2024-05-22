using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class BaseEnemyView : MonoBehaviour
{
    public bool OnHand;

    [SerializeField] private Collider collider;
    [SerializeField] private BaseEnemyModel _model;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _shootPoint;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        _model = GetComponent<BaseEnemyModel>();

        UnshowPunchCollider();
        
        OnHand = false;

        //EnemyEventManager.ShootEvent += StartAttackAnimation;
    }

    public void StartAttackAnimation()
    {
        if (anim != null && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0))
            anim.SetTrigger("Attack");
    }

    private void Shoot()
    {
        BulletMovement arrow = Instantiate(_projectile, _shootPoint.position, Quaternion.Euler(new Vector3(0, 0, 90))).GetComponent<BulletMovement>();

        arrow.Target = _model._currentBuilding;
    }

    public void ShowPunchCollider()
    {
        if(collider != null)
            collider.enabled = true;
    }

    public void UnshowPunchCollider()
    {
        if (collider != null)
            collider.enabled = false;
    }

    public void EnemyOnHand()
    {
        OnHand = true;
    }

    public void EnemyOffHand()
    {
        OnHand = false;
    }
}
