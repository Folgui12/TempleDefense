using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBoxController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("chau´pete");
        WaveSpawner.Instance.RemoveEnemy(other.gameObject);
        other.gameObject.GetComponent<BaseEnemyModel>().TakeDamage(9999);
        //Destroy(other.gameObject);
    }
}
