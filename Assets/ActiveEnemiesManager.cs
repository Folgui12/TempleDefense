using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveEnemiesManager : MonoBehaviour
{
    public static ActiveEnemiesManager Instance;

    public GameObject[] activeEnemies;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else 
            Destroy(Instance);
    }

    public void GetAllActiveEnemies()
    {
        activeEnemies = GameObject.FindGameObjectsWithTag("GenericEnemy");
    }
}
