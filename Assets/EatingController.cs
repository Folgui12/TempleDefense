using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Consumable consumable = other.GetComponent<Consumable>();
        if (consumable.canEat && consumable != null)
        {
            //SFX de cosas comibles con el codigo de Consumable
            Debug.Log("COMIDA");
            other.gameObject.GetComponent<BaseEnemyModel>().TakeDamage(99999);
        }
        else if(!consumable.canEat && consumable != null)
        {
            Debug.Log("NO COMIDA");
            //SFX de cosas no comibles con el codigo de Consumable
        }
    }
}
