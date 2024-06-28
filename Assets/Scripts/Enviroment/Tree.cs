using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] GridCollider _grid;
    private void Awake()
    {
        _grid = GetComponent<GridCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("golem"))
        {
            Debug.Log(_grid);
            _grid.KillCollider();
        }
    }
}
