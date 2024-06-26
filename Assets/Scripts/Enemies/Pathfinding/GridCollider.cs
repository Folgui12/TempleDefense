using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCollider : MonoBehaviour
{
    private void Start()
    {
        MyGrid.instance.AddCollider(GetComponent<Collider>());
    }

    private void OnDisable()
    {
        MyGrid.instance.RemoveCollider(GetComponent<Collider>());
    }
}
