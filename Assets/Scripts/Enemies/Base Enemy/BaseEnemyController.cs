using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyController : MonoBehaviour
{
    BaseEnemyModel _model;

    private void Awake()
    {
        _model = GetComponent<BaseEnemyModel>();
    }

    private void Update()
    {
        if (_model.CheckDistance())
            _model.Attack();
        else
        {
            _model.CheckClosest();
            if (_model.CheckYPosition())
                _model.GoToMainBuilding();
            Debug.Log("Raid");
        }
    }
}
