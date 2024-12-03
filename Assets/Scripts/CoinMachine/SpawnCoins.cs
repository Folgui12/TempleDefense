using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoins : ManagedUpdateBehavior
{
    [SerializeField] private GameObject TypeOfCoin;
    [SerializeField] private Transform SpawnPoint;
    [SerializeField] private float TimeBetweenPurchase;
    [SerializeField] private Transform platform;

    private DefenseType coinToSpawn;

    private float timer;
    private bool CanStartTimer = false;
    private bool CanBuyCoin = true;

    [SerializeField] private float impulse;

    override protected void Start()
    {
        base.Start();
        coinToSpawn = TypeOfCoin.GetComponent<TypeOfDefenseCoin>().defenseType;
    }

    protected override void CustomLightUpdate()
    {
        base.CustomLightUpdate();
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
                GameObject coin = Instantiate(TypeOfCoin, SpawnPoint.position, SpawnPoint.rotation);
                LanzarHaciaObjetivo(coin);
                CanBuyCoin = false;
                CanStartTimer = true;
            }

            else
            {
                Debug.Log("Not Enought Money");
            }
        }
    }

    public void LanzarHaciaObjetivo(GameObject coin)
    {
        Rigidbody coinRB = coin.GetComponent<Rigidbody>();
        Vector3 direction = coin.transform.position - platform.transform.position;

        float distance = direction.magnitude;

        float maxHeight = GetMaxHeight();

        float speed = Mathf.Sqrt(distance * Mathf.Abs(Physics.gravity.y) * maxHeight / (distance * 2));

        Vector3 normalizeDirection = direction.normalized;
        float horizontalSpeed = (speed * normalizeDirection.x * -1);
        float verticalSpeed = (speed * normalizeDirection.y) * maxHeight;

        coinRB.velocity = new Vector3(horizontalSpeed, verticalSpeed, direction.z) * impulse;
    }
    private float GetMaxHeight()
    {
        float randoHeight = Random.Range(8, 15);
        return randoHeight;
    }
}
