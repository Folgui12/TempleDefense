using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HammerController : MonoBehaviour
{
    public bool OnHand;

    public void HammerOnHand()
    {
        OnHand = true;
    }

    public void HammerOffHand() 
    { 
        OnHand = false; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            other.gameObject.GetComponent<TowerModel>().TakeDamage(99999);
        }
        if(other.gameObject.layer == 3 || other.gameObject.layer == 17)
        {
            other.gameObject.GetComponent<BaseEnemyModel>().TakeDamage(10);
        }
    }
}
