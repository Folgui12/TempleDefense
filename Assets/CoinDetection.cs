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
        if (other.gameObject.CompareTag("Coin") && spawner.canSpawnDefense)
        {
            BuildingArea.GetComponent<MeshRenderer>().material = solidMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Coin") && spawner.canSpawnDefense)
        {
            BuildingArea.GetComponent<MeshRenderer>().material = transparentMaterial;
        }
    }
}
