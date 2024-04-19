using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporalLife : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            BaseEnemyModel _enemyModel = collision.gameObject.GetComponent<BaseEnemyModel>();
            _enemyModel._currentBuilding = _enemyModel._mainBuilding;
            Debug.Log(_enemyModel._currentBuilding);
            Destroy(gameObject);
        }
    }
}
