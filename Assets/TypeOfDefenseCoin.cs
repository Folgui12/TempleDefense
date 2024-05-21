using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeOfDefenseCoin : MonoBehaviour
{
    public DefenseType defenseType;

    public bool OnHand;

    public void CoinOnHand()
    {
        OnHand = true;
    }

    public void CoinOffHand()
    {
        OnHand = false;
    }
}
