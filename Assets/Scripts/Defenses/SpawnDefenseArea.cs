using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDefenseArea : MonoBehaviour
{
    public List<GameObject> Defenses;
    Dictionary<DefenseType, GameObject> typeOfDefenses; 
    public bool canSpawnDefense;
    public bool canSellDefense;


    // Start is called before the first frame update
    void Start()
    {
        canSpawnDefense = true;
        canSellDefense = false;

        typeOfDefenses = new Dictionary<DefenseType, GameObject>();

        for(int i = 0; i < Defenses.Count; i++)
        {
            DefenseType d = Defenses[i].GetComponent<TowerModel>()._stats.Type;

            typeOfDefenses[d] = Defenses[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Coin") && canSpawnDefense)
        {
           DefenseType newDefense = other.gameObject.GetComponent<TypeOfDefenseCoin>().defenseType;

           if(typeOfDefenses.ContainsKey(newDefense))
           {
                Instantiate(typeOfDefenses[newDefense], transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
           }

           canSpawnDefense = false;

           canSellDefense = true;

           Destroy(other.gameObject);
        }
    }
}
