using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager: MonoBehaviour
{
    public static CurrencyManager Instance;
    public int moneyCount;
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshValues()
    {
        money.text = moneyCount.ToString();
        prayers.text = prayersCount.ToString();
    }

    public void AddMoney(int value)
    {
        moneyCount = value;
        RefreshValues();
    }

    public void AddPrayers(int value)
    {
        prayersCount = value;
        RefreshValues();
    }
}
