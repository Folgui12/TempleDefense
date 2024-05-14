using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyView : MonoBehaviour
{
    [SerializeField] private Collider collider;
    Animator anim; 

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        UnshowPunchCollider();
    }

    public void StartAttackAnimation()
    {
        anim.SetTrigger("Attack");
    }

    public void ShowPunchCollider()
    {
        collider.enabled = true;
    }

    public void UnshowPunchCollider()
    {
        collider.enabled = false;
    }
}
