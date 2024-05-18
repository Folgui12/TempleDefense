using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoins : MonoBehaviour
{
    [SerializeField] private GameObject TypeOfCoin;
    [SerializeField] private Transform SpawnPoint;
    public void Spawn()
    {
        Instantiate(TypeOfCoin, SpawnPoint.position, SpawnPoint.rotation);
    }
}
