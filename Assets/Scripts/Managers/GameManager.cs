using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float minimunTimeToLearn;
    [SerializeField] private GameObject aWayOut;
    
    public static GameManager Instance;

    public bool firstTutoFinish;

    private float timer;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        //NextRoundAux();
        firstTutoFinish = false;
    }

    private void Update()
    {
        if(firstTutoFinish) 
        {
            if(timer > minimunTimeToLearn) 
            {
                aWayOut.SetActive(true);
                firstTutoFinish = false;
            }

            timer += Time.deltaTime;
        }
    }

    public void NextRoundAux()
    {
        WaveSpawner.Instance.NextWave();
    }
}
