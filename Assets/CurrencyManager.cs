using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager: MonoBehaviour
{
    public static CurrencyManager Instance;

    public int MoneyCount => moneyCount;
    public int PrayersCount => prayersCount;

    private int moneyCount;
    public int prayersCount;

    public Text money;
    public Text prayers;

    public int miniTemplesCount;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        RefreshValues();
    }

    public void RefreshValues()
    {
        money.text = moneyCount.ToString();
        prayers.text = prayersCount.ToString();
    }

    public void AddMoney(int value)
    {
        moneyCount += value;
        RefreshValues();
    }

    public void RemoveMoney(int value)
    {
        if(moneyCount > value)
            moneyCount -= value;

        RefreshValues();
    }

    public void AddPrayers(int value)
    {
        prayersCount += value;
        RefreshValues();
    }

    public void RemovePrayers(int value)
    {
        if(prayersCount > value)
            prayersCount -= value;

        RefreshValues();
    }
}
