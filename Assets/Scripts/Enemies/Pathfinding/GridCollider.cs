using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCollider : MonoBehaviour
{
    Collider _collider;
    private void Start()
    {
        MyGrid.singleton.AddCollider(GetComponent<Collider>());
        _collider = GetComponent<Collider>();
    }
    public void KillCollider()
    {
        MyGrid.singleton.RemoveCollider(_collider);
        Destroy(gameObject);
    }
}
