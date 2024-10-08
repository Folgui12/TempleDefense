using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDefenseArea : MonoBehaviour
{
    public List<GameObject> Defenses;
    Dictionary<DefenseType, GameObject> typeOfDefenses; 

    public bool canSellDefense;
    public DefenseStats CurrentDefense => currentDefense;

    [SerializeField] private Material solidMaterial;
    [SerializeField] private Material transparentMaterial;

    private DefenseStats currentDefense;

    private TypeOfDefenseCoin coin;

    private bool alreadyWithDefense;


    // Start is called before the first frame update
    void Start()
    {
        alreadyWithDefense = false; 

        typeOfDefenses = new Dictionary<DefenseType, GameObject>();

        for(int i = 0; i < Defenses.Count; i++)
        {
            DefenseType d = Defenses[i].GetComponent<TowerModel>()._stats.Type;

            typeOfDefenses[d] = Defenses[i];
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Coin") && !alreadyWithDefense)
        {
            coin = other.gameObject.GetComponent<TypeOfDefenseCoin>();

            if(coin.OnHand)
            {
                setSolidMaterial();
            }

            else
            {
                DefenseType newDefense = coin.defenseType;

                if(typeOfDefenses.ContainsKey(newDefense))
                {
                    var defense = Instantiate(typeOfDefenses[newDefense], transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));

                    defense.transform.parent = gameObject.transform;    

                    currentDefense = defense.GetComponent<TowerModel>()._stats;

                    alreadyWithDefense = true;
                }

                Destroy(other.gameObject);
            }
            
        }
    }

    public void CanSpawnAgain()
    {
        alreadyWithDefense = false;
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Coin"))
        {
            setTransparentMaterial();
        }
    }

    public void setSolidMaterial()
    {
        this.GetComponent<MeshRenderer>().material = solidMaterial;
    }

    public void setTransparentMaterial()
    {
        this.GetComponent<MeshRenderer>().material = transparentMaterial;
    }
}
