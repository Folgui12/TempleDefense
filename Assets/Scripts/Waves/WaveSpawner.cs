using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : ManagedUpdateBehavior
{
    public static WaveSpawner Instance;

    public Text WaveCounter; 

    public List<Enemy> enemies = new List<Enemy>();
    public int MaxRando = 1;

    public int currWave;
    public int waveValue;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();

    public GameObject enemy;

    public Transform[] spawnLocation;
    public int spawnIndex;
 
    public int waveDuration;
    public float spawnInterval;
    public float spawnTimer;

    public ObjectPoolSatiro poolSatiro;
    public ObjectPoolCentauro poolCentauro;
    public ObjectPoolGolem poolGolem;

    public List<GameObject> spawnedEnemies = new List<GameObject>();

    [SerializeField] private GameObject VFX;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(this);

        poolSatiro.Pool(enemies[0].enemyPrefab, 1);
        poolCentauro.Pool(enemies[1].enemyPrefab, 1);
        poolGolem.Pool(enemies[2].enemyPrefab, 1);
    }
    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        NextWave();
        //UpdateWavevCounter();
    }

    override protected void CustomLightFixedUpdate()
    {
        base.CustomLightFixedUpdate();
        if (spawnTimer <=0)
        {
            //spawn an enemy
            if(enemiesToSpawn.Count > 0)
            {
                Instantiate(VFX, spawnLocation[spawnIndex].transform.position, spawnLocation[spawnIndex].transform.rotation);
                enemy = enemiesToSpawn[0];

                if (enemy == enemies[0].enemyPrefab)
                {
                    poolSatiro.GetPooled(spawnLocation[spawnIndex], enemy);
                }
                if (enemy == enemies[1].enemyPrefab)
                {
                    poolCentauro.GetPooled(spawnLocation[spawnIndex], enemy);
                }
                if (enemy == enemies[2].enemyPrefab)
                {
                    poolGolem.GetPooled(spawnLocation[spawnIndex], enemy);
                }

                enemiesToSpawn.RemoveAt(0); // and remove it
                spawnedEnemies.Add(enemy);
                spawnTimer = spawnInterval;

                spawnIndex = Random.Range(0, spawnLocation.Length);
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
        }
        ActiveEnemiesManager.Instance.GetAllActiveEnemies();

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
        {
            Debug.Log("pete");
            spawnedEnemies.Remove(enemy);
        }

        if (enemy.name == "Satiro(Clone)")
        {
            Debug.Log("Satiro");
            poolSatiro.ReturnToPool(enemy);
        }
        if (enemy.name == "Centauro(Clone)")
        {
            Debug.Log("Centauro");
            poolCentauro.ReturnToPool(enemy);
        }
        if (enemy.name == "Golem(Clone)")
        {
            Debug.Log("Golem");
            poolGolem.ReturnToPool(enemy);
        }
        spawnedEnemies.Remove(enemy);
    }

    public void GenerateWave()
    {
        if(currWave > 0)
        {
            waveValue = currWave * 10;
            GenerateEnemies();
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

        if (currWave >= 5 && MaxRando < enemies.Count)
        {
            MaxRando += 1;
        }
        List<GameObject> generatedEnemies = new List<GameObject>();
        while(waveValue>0 || generatedEnemies.Count < 50)
        {
            int randEnemyId = Random.Range(0, MaxRando);
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
