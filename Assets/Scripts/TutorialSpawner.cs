using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSpawner : MonoBehaviour
{
    public static TutorialSpawner Instance;

    [SerializeField] private GameObject Satiro;
    [SerializeField] private GameObject Centauro;
    [SerializeField] private GameObject Golem;

    private bool IsSatiroSpawn;
    private bool IsCentauroSpawn;
    private bool IsGolemSpawn;

    public ObjectPoolSatiro poolSatiro;
    public ObjectPoolCentauro poolCentauro;
    public ObjectPoolGolem poolGolem;

    [SerializeField] private Transform SpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(this);

        poolSatiro.Pool(Satiro, 1);
        poolCentauro.Pool(Centauro, 1);
        poolGolem.Pool(Golem, 1);
    }

    public void SpawnSatire()
    {
        if (!IsSatiroSpawn)
        {
            poolSatiro.GetPooled(SpawnPoint, Satiro);
            IsSatiroSpawn = true;

        }
    }

    public void SpawnCentauro()
    {
        if (!IsCentauroSpawn)
        {
            poolCentauro.GetPooled(SpawnPoint, Centauro);
            IsCentauroSpawn = true;

        }
    }

    public void SpawnGolem()
    {
        if (!IsGolemSpawn)
        {
            poolGolem.GetPooled(SpawnPoint, Golem);
            IsGolemSpawn = true;
        }
    }


    public void RemoveEnemy(GameObject enemy)
    {
        if (enemy.name == "Satiro(Clone)")
        {
            Debug.Log("Satiro");
            poolSatiro.ReturnToPool(enemy);
            IsSatiroSpawn = false;
        }
        if (enemy.name == "Centauro(Clone)")
        {
            Debug.Log("Centauro");
            poolCentauro.ReturnToPool(enemy);
            IsCentauroSpawn = false;
        }
        if (enemy.name == "Golem(Clone)")
        {
            Debug.Log("Golem");
            poolGolem.ReturnToPool(enemy);
            IsGolemSpawn = false;
        }
    }

}
