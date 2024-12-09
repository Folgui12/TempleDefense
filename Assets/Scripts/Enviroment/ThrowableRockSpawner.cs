using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableRockSpawner : MonoBehaviour
{
    public bool roundActive;

    [SerializeField] private GameObject rock;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float timeBetweenSpawn;
    [SerializeField] private int MaxRocksActive;
    [SerializeField] private List<GameObject> Rocks = new();

    private float timer;

    private int activeRocks;
    


    // Start is called before the first frame update
    void Start()
    {
        CreateRocks();
    }

    // Update is called once per frame
    void Update()
    {
        if (roundActive)
        {
            timer += Time.deltaTime;

            if(timer > timeBetweenSpawn && activeRocks < MaxRocksActive)
            {
                SpawnRock();

                timer = 0;
            }
        }
    }

    private void SpawnRock()
    {
        foreach (GameObject rock in Rocks)
        {
            if(!rock.activeInHierarchy)
            {
                rock.transform.position = spawnPoint.position;
                rock.SetActive(true);
                return;
            }
        }
    }

    public void RoundStarted()
    {
        roundActive = true;
    }

    private void CreateRocks()
    {
        int aux = 0;

        while(aux < MaxRocksActive)
        {
            GameObject newRock = Instantiate(rock, Vector3.zero, rock.transform.rotation);
            newRock.SetActive(false);

            Rocks.Add(newRock);

            aux++;
        }
        

    }
}
