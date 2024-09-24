using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolFlyingEnemy : MonoBehaviour
{
    public Queue<GameObject> pooledObjects;

    // Start is called before the first frame update
    public void Pool(GameObject _objects, int poolSize)
    {
        pooledObjects = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject objects = Instantiate(_objects);
            objects.SetActive(false);
            pooledObjects.Enqueue(objects);
        }
    }
    private void AddToPool(GameObject _objects)
    {
        GameObject objects = Instantiate(_objects);
        objects.SetActive(false);
        pooledObjects.Enqueue(objects);
    }
    public GameObject GetPooled(Transform transform, GameObject _objects)
    {
        if (pooledObjects.Count <= 0)
        {
            AddToPool(_objects);
        }
        if (pooledObjects.Count > 0)
        {
            _objects = pooledObjects.Dequeue();
            _objects.transform.position = transform.position;
            _objects.SetActive(true);
            return _objects;
        }
        return null;
    }
    public void ReturnToPool(GameObject _objects)
    {
        _objects.SetActive(false);
        pooledObjects.Enqueue(_objects);
    }
}
