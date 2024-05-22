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


    // Start is called before the first frame update
    void Start()
    {
        canSellDefense = false;

        typeOfDefenses = new Dictionary<DefenseType, GameObject>();

        for(int i = 0; i < Defenses.Count; i++)
        {
            DefenseType d = Defenses[i].GetComponent<TowerModel>()._stats.Type;

            typeOfDefenses[d] = Defenses[i];
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Coin") && currentDefense == null)
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

                        currentDefense = defense.GetComponent<TowerModel>()._stats;
                }

                canSellDefense = true;

                Destroy(other.gameObject);
            }
            
        }
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
