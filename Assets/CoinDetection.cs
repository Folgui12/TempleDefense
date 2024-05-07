using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDetection : MonoBehaviour
{
    [SerializeField] private Material solidMaterial;
    [SerializeField] private Material transparentMaterial;
    [SerializeField] private GameObject BuildingArea;
    [SerializeField] private SpawnDefenseArea spawner;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Coin") && spawner.CurrentDefense == null)
        {
            setSolidMaterial();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Coin") && spawner.CurrentDefense == null)
        {
            setTransparentMaterial();
        }
    }

    public void setSolidMaterial()
    {
        BuildingArea.GetComponent<MeshRenderer>().material = solidMaterial;
    }

    public void setTransparentMaterial()
    {
        BuildingArea.GetComponent<MeshRenderer>().material = transparentMaterial;
    }
}
