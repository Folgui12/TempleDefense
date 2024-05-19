using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoins : MonoBehaviour
{
    [SerializeField] private GameObject TypeOfCoin;
    [SerializeField] private Transform SpawnPoint;

    private TypeOfDefenseCoin coinToSpawn;

    void Start()
    {
        coinToSpawn = TypeOfCoin.GetComponent<TypeOfDefenseCoin>();
    }

    public void Spawn()
    {
        if(CurrencyManager.Instance.MoneyCount > coinToSpawn.defenseType.price)
        {
            CurrencyManager.Instance.RemoveMoney(coinToSpawn.defenseType.price);
            Instantiate(TypeOfCoin, SpawnPoint.position, SpawnPoint.rotation);
        }

        else
        {   
            Debug.Log("Not Enought Money");
        }
    }
}
