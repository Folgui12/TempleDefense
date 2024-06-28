using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCollider : MonoBehaviour
{
    Collider _collider;
    private void Start()
    {
        MyGrid.instance.AddCollider(GetComponent<Collider>());
        _collider = GetComponent<Collider>();
    }
    public void KillCollider()
    {
        MyGrid.instance.RemoveCollider(_collider);
        Destroy(gameObject);
    }
}
