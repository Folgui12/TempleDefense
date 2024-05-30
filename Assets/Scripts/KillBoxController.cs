using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBoxController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("chau´pete");
        WaveSpawner.Instance.RemoveEnemy(other.gameObject);
        Destroy(other.gameObject);
    }
}
