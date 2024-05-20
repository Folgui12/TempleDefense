using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoins : MonoBehaviour
{
    [SerializeField] private GameObject TypeOfCoin;
    [SerializeField] private Transform SpawnPoint;

    private DefenseType coinToSpawn;

    void Start()
    {
        coinToSpawn = TypeOfCoin.GetComponent<TypeOfDefenseCoin>().defenseType;
    }

    public void Spawn()
    {
        Debug.Log(coinToSpawn.price);

        if(CurrencyManager.Instance.MoneyCount > coinToSpawn.price)
        {
            CurrencyManager.Instance.RemoveMoney(coinToSpawn.price);
            Instantiate(TypeOfCoin, SpawnPoint.position, SpawnPoint.rotation);
        }

        else
        {   
            Debug.Log("Not Enought Money");
        }
    }
}
