using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDefenseArea : MonoBehaviour
{
    [SerializeField] private CoinDetection coinDetection;
    public List<GameObject> Defenses;
    Dictionary<DefenseType, GameObject> typeOfDefenses; 
    //public bool canSpawnDefense;
    public bool canSellDefense;
    public DefenseStats CurrentDefense => currentDefense;

    private DefenseStats currentDefense;


    // Start is called before the first frame update
    void Start()
    {
        //canSpawnDefense = true;
        canSellDefense = false;

        typeOfDefenses = new Dictionary<DefenseType, GameObject>();

        for(int i = 0; i < Defenses.Count; i++)
        {
            DefenseType d = Defenses[i].GetComponent<TowerModel>()._stats.Type;

            typeOfDefenses[d] = Defenses[i];
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Coin") && currentDefense == null)
        {
           DefenseType newDefense = other.gameObject.GetComponent<TypeOfDefenseCoin>().defenseType;

           if(typeOfDefenses.ContainsKey(newDefense))
           {
                var defense = Instantiate(typeOfDefenses[newDefense], transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));

                currentDefense = defense.GetComponent<TowerModel>()._stats;
           }

           //canSpawnDefense = false;

           canSellDefense = true;

           coinDetection.setTransparentMaterial();

           Destroy(other.gameObject);
        }
    }
}
