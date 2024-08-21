using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolArrows : MonoBehaviour
{
    public Queue<GameObject> pooledObjects;
    public GameObject arrow;
    private void Start()
    {
        Pool(arrow, 30);
    }
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
    public GameObject GetPooled(Transform transform, GameObject _objects, Quaternion rotation)
    {
        if (pooledObjects.Count > 0)
        {
            _objects = pooledObjects.Dequeue();
            _objects.transform.position = transform.position;
            _objects.transform.rotation = rotation;
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
