using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDefenseArea : MonoBehaviour
{
    public List<GameObject> Defenses;
    Dictionary<DefenseType, GameObject> typeOfDefenses; 


    // Start is called before the first frame update
    void Start()
    {
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
        if(other.gameObject.CompareTag("Coin"))
        {
           DefenseType newDefense = other.gameObject.GetComponent<TypeOfDefenseCoin>().defenseType;

           if(typeOfDefenses.ContainsKey(newDefense))
           {
                Instantiate(typeOfDefenses[newDefense], transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
           }

           Destroy(other.gameObject);
        }
    }
}
