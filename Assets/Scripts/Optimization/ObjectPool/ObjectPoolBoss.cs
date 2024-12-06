using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolBoss : MonoBehaviour
{
    public Queue<GameObject> pooledObjects;
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
            var instance = pooledObjects.Dequeue();
            instance.transform.position = transform.position;
            instance.SetActive(true);
            return instance;
        }
        return null;
    }
    public void ReturnToPool(GameObject _objects)
    {
        _objects.SetActive(false);
        pooledObjects.Enqueue(_objects);
    }
}
