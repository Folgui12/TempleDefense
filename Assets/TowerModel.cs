using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerModel : MonoBehaviour
{
    public DefenseStats _stats;

    public GameObject _currentEnemy;
    
    LoS _los;
    
    // Start is called before the first frame update
    void Start()
    {
        _los = GetComponent<LoS>();
    }
    
    public GameObject CheckClosestEnemy()
    {
        Collider[] colliderList = Physics.OverlapSphere(transform.position, _stats._attackRange);

        for(int i = 0; i < colliderList.Length; i++)
        {
            if (colliderList[i].tag == "Enemy" && _los.CheckRange(colliderList[i].transform, _stats._attackRange))
            {
                _currentEnemy = colliderList[i].gameObject;
            }
        }
        return _currentEnemy;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;      
        Gizmos.DrawWireSphere(transform.position, _stats._attackRange);
    }
}
