using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MyGrid : MonoBehaviour
{
    public bool skipY;
    Dictionary<Vector3, int> _dic = new Dictionary<Vector3, int>();
    public static MyGrid instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    public void AddCollider(Collider collider)
    {
        var points = GetPointsInCollider(collider, skipY);
        for (int i = 0; i < points.Count; i++)
        {
            if (_dic.ContainsKey(points[i]))
            {
                _dic[points[i]] += 1;
            }
            else
            {
                _dic[points[i]] = 1;
            }
        }
    }
    public void RemoveCollider(Collider collider)
    {
        var points = GetPointsInCollider(collider, skipY);
        Debug.Log(points.Count);
        for (int i = 0; i < points.Count; i++)
        {
            Debug.Log("Principio");
            if (_dic.ContainsKey(points[i]))
            {
                Debug.Log("Mitad");
                _dic[points[i]] -= 1;
                if (_dic[points[i]] <= 0)
                {
                    Debug.Log("final");
                    _dic.Remove(points[i]);
                }
            }
        }
    }
    public Vector3 GetPosInGrid(Vector3 pos)
    {
        return Vector3Int.RoundToInt(pos);
    }
    public bool IsRightPos(Vector3 pos)
    {
        return !_dic.ContainsKey(pos);
    }
    List<Vector3> GetPointsInCollider(Collider collider, bool skipY = false)
    {
        List<Vector3> points = new List<Vector3>();
        Bounds bounds = collider.bounds;
        Debug.Log(bounds);
        int minX = Mathf.FloorToInt(bounds.min.x);
        int maxX = Mathf.CeilToInt(bounds.max.x);
        int minY = skipY ? 0 : Mathf.FloorToInt(bounds.min.y);
        int maxY = skipY ? 0 : Mathf.CeilToInt(bounds.max.y);
        int minZ = Mathf.FloorToInt(bounds.min.z);
        int maxZ = Mathf.CeilToInt(bounds.max.z);

        for (int x = minX; x <= maxX; x++)
        {
            Debug.Log("l");
            for (int y = minY; y <= maxY; y++)
            {
                Debug.Log("o");
                for (int z = minZ; z <= maxZ; z++)
                {
                    Debug.Log("l");
                    Vector3 point = new Vector3(x, y, z);
                    Debug.Log(point);
                    if (bounds.Contains(point))
                    {
                        points.Add(point);
                    }
                }
            }
        }
        return points;
    }
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.Red;
        foreach (var item in _dic)
        {
            Gizmos.DrawWireSphere(item.Key, 0.25f);
        }
    }
}
