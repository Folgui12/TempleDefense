using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HammerController : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        other.gameObject.GetComponent<TowerModel>().TakeDamage(99999);
    }
}
