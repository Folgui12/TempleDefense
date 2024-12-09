using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyView : MonoBehaviour
{

    [SerializeField] private new Collider collider;
    [SerializeField] private BaseEnemyModel _model;
    [SerializeField] private GameObject _projectile;

    public bool Attacking;

    [SerializeField] private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        _model = GetComponent<BaseEnemyModel>();

        UnshowPunchCollider();
    }

    public void StartAttackAnimation()
    {
        //if (anim != null && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0))
        //    anim.SetTrigger("Attack");
        if (Attacking == true)
        {
            StartCoroutine(AnimAttack());
        }
        else if (Attacking == false)
        {
            StopCoroutine(AnimAttack());
        }
    }

    public IEnumerator AnimAttack()
    {
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds (_model._stats.attackSpeed);
    }
    public void CanIdle()
    {
        anim.SetBool("InRange", true);
    }

    public void StopIdle()
    {
        anim.SetBool("InRange", false);
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

    public void DoBossDamage()
    {
        int explotionRad = 10;

        Debug.Log(transform.position);

        var surroundedEnemiesTrees = Physics.OverlapSphere(transform.position - new Vector3(0, 5, 0), explotionRad);

        foreach(var collision in surroundedEnemiesTrees)
        {
            Tree detectTree = collision.GetComponent<Tree>();
            TowerModel detectDefense = collision.GetComponent<TowerModel>();
            MainBuildingManager mainBuilding = collision.GetComponent<MainBuildingManager>();

            if(detectTree != null)
            {
                Destroy(detectTree.gameObject);
            }
            else if(detectDefense != null)  
            {
                detectDefense.TakeDamage(_model._stats.Damage);
            }
            else if(mainBuilding != null)
            {
                mainBuilding.TakeDamage(_model._stats.Damage);
            }
        }
    }
}
