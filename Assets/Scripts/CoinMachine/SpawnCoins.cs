using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoins : MonoBehaviour
{
    [SerializeField] private GameObject TypeOfCoin;
    [SerializeField] private Transform SpawnPoint;
    [SerializeField] private float TimeBetweenPurchase;

    private DefenseType coinToSpawn;

    private float timer;
    private bool CanStartTimer = false;
    private bool CanBuyCoin = true;

    void Start()
    {
        coinToSpawn = TypeOfCoin.GetComponent<TypeOfDefenseCoin>().defenseType;
    }

    private void Update()
    {
        if(CanStartTimer)
        {
            timer += Time.deltaTime;
        }

        if(timer >= TimeBetweenPurchase)
        {
            CanBuyCoin = true;
            CanStartTimer = false;
            timer = 0;
        }
    }

    public void Spawn()
    {
        if(CanBuyCoin)
        {
            if (CurrencyManager.Instance.MoneyCount >= coinToSpawn.price)
            {
                CurrencyManager.Instance.RemoveMoney(coinToSpawn.price);
                Instantiate(TypeOfCoin, SpawnPoint.position, SpawnPoint.rotation);
                CanBuyCoin = false;
                CanStartTimer = true;
            }

            else
            {
                Debug.Log("Not Enought Money");
            }
        }
        
    }
}
