using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : ManagedUpdateBehavior
{
    public static WaveSpawner Instance;
    //public MusicManager musicManager;

    public Text WaveCounter;

    public List<Enemy> enemies = new List<Enemy>();
    public int MaxRando = 1;

    public int currWave;
    [SerializeField] private int waveValue;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();

    public GameObject enemy;

    public Transform[] spawnLocation;
    public int spawnIndex;
 
    public int waveDuration;
    public float spawnInterval;
    public float spawnTimer;
    public int WaveMultyplier;

    public ObjectPoolSatiro poolSatiro;
    public ObjectPoolCentauro poolCentauro;
    public ObjectPoolGolem poolGolem;
    public ObjectPoolBoss poolBoss;

    public bool canHitButton;
    public ButtonBehaviour NextRoundButton;
    private int lastGeneratedWave = -1;

    [SerializeField] private GameObject VFX;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(this);

        poolSatiro.Pool(enemies[0].enemyPrefab, 1);
        poolCentauro.Pool(enemies[1].enemyPrefab, 1);
        poolGolem.Pool(enemies[2].enemyPrefab, 1);
        poolBoss.Pool(enemies[3].enemyPrefab, 1);

    }
    override protected void Start()
    {
        base.Start();
        //NextWave();
        canHitButton = false;
    }
    override protected void CustomLightFixedUpdate()
    {
        base.CustomLightFixedUpdate();
        if (spawnTimer <=0)
        {

            //spawn an enemy
            if (enemiesToSpawn.Count > 0)
            {
                Instantiate(VFX, spawnLocation[spawnIndex].transform.position, VFX.transform.rotation);
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
                if (enemy == enemies[3].enemyPrefab)
                {
                    poolBoss.GetPooled(spawnLocation[spawnIndex], enemy);
                }

                canHitButton = false;

                enemiesToSpawn.RemoveAt(0); // and remove it
                spawnTimer = spawnInterval;

                spawnIndex = Random.Range(0, spawnLocation.Length);
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
        }

        ActiveEnemiesManager.Instance.GetAllActiveEnemies();

        if (!canHitButton && ActiveEnemiesManager.Instance.activeEnemies.Length <= 0 && enemiesToSpawn.Count <= 0)
        {
            canHitButton = true;
            //musicManager.StopMusic();
            NextRoundButton.FinishRound();
        }
    }

    public void NextWave()
    {

        if (ActiveEnemiesManager.Instance.activeEnemies.Length <= 0 && enemiesToSpawn.Count <= 0)
        {
            currWave++;
            UpdateWavevCounter();
            GenerateWave();
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        if (enemy.name == "Satiro(Clone)")
        {
            poolSatiro.ReturnToPool(enemy);
        }
        if (enemy.name == "Centauro(Clone)")
        {
            poolCentauro.ReturnToPool(enemy);
        }
        if (enemy.name == "Golem(Clone)")
        {
            poolGolem.ReturnToPool(enemy);
        }
        ActiveEnemiesManager.Instance.GetAllActiveEnemies();
    }

    public void GenerateWave()
    {
        if(currWave > 0)
        {
            waveValue = currWave * WaveMultyplier;
            GenerateEnemies();
        }
    }
 
    public void GenerateEnemies()
    {
        if (currWave >= 5 && MaxRando < enemies.Count)
        {
            MaxRando = 2;
        }
        List<GameObject> generatedEnemies = new List<GameObject>();
        if (currWave % 10 == 0 && lastGeneratedWave != currWave)
        {
            generatedEnemies.Add(enemies[3].enemyPrefab);
        }
        while (waveValue>0 || generatedEnemies.Count < 50)
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
