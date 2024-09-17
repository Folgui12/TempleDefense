using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyPos : MonoBehaviour
{
    [SerializeField] private Transform FloorPos;
    [SerializeField] private BaseEnemyModel model;

    [SerializeField] private float TransformAir;
    [SerializeField] private Animator animator;

    void Update()
    {
        if (model.gameObject.activeInHierarchy && !model.OnHand)
        {
            if(model.OnGround)
            {
                transform.position = new Vector3(FloorPos.position.x, TransformAir, FloorPos.position.z);
                
                Debug.Log("Moving");
            }
        }
        else
        {
            model.gameObject.SetActive(false);
        }
    }

    public void MoveModel()
    {
        //FloorPos.position = new Vector3(transform.position.x, FloorPos.position.y, transform.position.z);
        model.gameObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.gameObject.CompareTag("Floor"))
        {
            FloorPos.position = new Vector3(transform.position.x, FloorPos.position.y, transform.position.z);
            model.gameObject.SetActive(true);
            animator.SetTrigger("HitGround");
        }
    }
}
