using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner Instance;

    public Text WaveCounter; 

    public List<Enemy> enemies = new List<Enemy>();
    public int currWave;
    public int waveValue;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();
 
    public Transform[] spawnLocation;
    public int spawnIndex;
 
    public int waveDuration;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;
 
    public List<GameObject> spawnedEnemies = new List<GameObject>();

    [SerializeField] private GameObject VFX;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        //UpdateWavevCounter();
    }
 
    // Update is called once per frame
    void FixedUpdate()
    {
        if(spawnTimer <=0)
        {
            //spawn an enemy
            if(enemiesToSpawn.Count > 0)
            {
                Instantiate(VFX, spawnLocation[spawnIndex].transform.position, spawnLocation[spawnIndex].transform.rotation);
                GameObject enemy = (GameObject)Instantiate(enemiesToSpawn[0], spawnLocation[spawnIndex].position,Quaternion.identity); // spawn first enemy in our list
                enemiesToSpawn.RemoveAt(0); // and remove it
                spawnedEnemies.Add(enemy);
                spawnTimer = spawnInterval;
 
                if(spawnIndex + 1 <= spawnLocation.Length-1)
                {
                    spawnIndex++;
                }
                else
                {
                    spawnIndex = 0;
                }
            }
            else
            {
                waveTimer = 0; // if no enemies remain, end wave
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
            waveTimer -= Time.fixedDeltaTime;
        }
 
        //if(waveTimer<=0 && spawnedEnemies.Count <=0)
        //{
        //    currWave++;
        //    GenerateWave();
        //}
    }
 
    public void NextWave()
    {
        currWave++;
        UpdateWavevCounter();
        GenerateWave();
        
    }

    public void RemoveEnemy(GameObject enemy)
    {
        if (spawnedEnemies.Contains(enemy))
            spawnedEnemies.Remove(enemy);
    }

    public void GenerateWave()
    {
        if(currWave > 0)
        {
            waveValue = currWave * 10;
            GenerateEnemies();

            spawnInterval = waveDuration / enemiesToSpawn.Count; // gives a fixed time between each enemies
            waveTimer = waveDuration; // wave duration is read only
        }
    }
 
    public void GenerateEnemies()
    {
        // Create a temporary list of enemies to generate
        // 
        // in a loop grab a random enemy 
        // see if we can afford it
        // if we can, add it to our list, and deduct the cost.
 
        // repeat... 
 
        //  -> if we have no points left, leave the loop
 
        List<GameObject> generatedEnemies = new List<GameObject>();
        while(waveValue>0 || generatedEnemies.Count <50)
        {
            int randEnemyId = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyId].cost;
 
            if(waveValue-randEnemyCost>=0)
            {
                generatedEnemies.Add(enemies[randEnemyId].enemyPrefab);
                waveValue -= randEnemyCost;
            }
            else if(waveValue<=0)
            {
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }

    private void UpdateWavevCounter()
    {
        WaveCounter.text = currWave.ToString();
    }
  
}
 
[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
}
