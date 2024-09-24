using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyView : MonoBehaviour
{

    public Collider col;
    [SerializeField] private BaseEnemyModel _model;
    [SerializeField] private GameObject _projectile;

    public bool Attacking;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        _model = GetComponent<BaseEnemyModel>();

        UnshowPunchCollider();
    }

    public void StartAttackAnimation()
    {
        if (anim != null && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0))
            anim.SetTrigger("Attack");
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
        if(col != null)
            col.enabled = true;
    }

    public void UnshowPunchCollider()
    {
        if (col != null)
            col.enabled = false;
    }

    public void StopAnim()
    {
        Debug.Log("asdawd");
        anim.enabled = false;
    }
}
