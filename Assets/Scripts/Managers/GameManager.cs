using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        NextRoundAux();
    }

    // Update is called once per frame
    void Update()
    {
        
    }   

    public void NextRoundAux()
    {
        WaveSpawner.Instance.NextWave();
    }
}
