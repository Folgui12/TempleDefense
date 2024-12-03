using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Consumable consumable = other.GetComponent<Consumable>();
        if(consumable != null)
        {
            if (consumable.canEat)
            {
                //SFX de cosas comibles con el codigo de Consumable
                Debug.Log("COMIDA");
                other.gameObject.GetComponent<BaseEnemyModel>().TakeDamage(99999);
            }
            else if (!consumable.canEat)
            {
                Debug.Log("NO COMIDA");
                //SFX de cosas no comibles con el codigo de Consumable
            }
        }
    }
}
