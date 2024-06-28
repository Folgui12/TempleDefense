using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyRaidState<T> : State<T>, IPoints
{
    //ISteering _steering;
    //BaseEnemyModel _model;
    //Transform _currentBiulding;
    //ObstacleAvoidance _obs;
    //public EnemyRaidState(BaseEnemyModel model, Transform target, ObstacleAvoidance obs, ISteering steering)
    //{
    //    _model = model;
    //    _currentBiulding = target;
    //    _obs = obs;
    //    _steering = steering;
    //}
    //public override void Execute()
    //{
    //    base.Execute();

    //    //A: Enemy
    //    //B: Target

    //    //(b-a).n
    //    GameObject currnetBiulding = _model.CheckClosest();
    //    var dir = _obs.GetDir(_steering.GetDir(), false);
    //    _model.Move(dir.normalized);
    //    _model.LookDir(dir.normalized);
    //}

    BaseEnemyModel _model;
    List<Vector3> _waypoints;
    int _nextPoint = 0;
    bool _isFinishPath = true;
    public EnemyRaidState(BaseEnemyModel model)
    {
        _model = model;
    }
    public override void Enter()
    {
        //_model._agentController.RunAStarPlusVector();
        base.Enter();
    }
    public override void Execute()
    {
        base.Execute();
        Run();
    }
    public override void Sleep()
    {
        base.Sleep();
    }
    public void SetWayPoints(List<Node> newPoints)
    {
        var list = new List<Vector3>();
        for (int i = 0; i < newPoints.Count; i++)
        {
            list.Add(newPoints[i].transform.position);
        }
        SetWayPoints(list);
    }
    public void SetWayPoints(List<Vector3> newPoints)
    {
        _nextPoint = 0;
        if (newPoints.Count == 0) return;
        //_anim.Play("CIA_Idle");
        _waypoints = newPoints;
        var pos = _waypoints[_nextPoint];
        pos.y = _model.transform.position.y;
        _model.SetPosition(pos);
        _isFinishPath = false;
    }
    void Run()
    {
        if (IsFinishPath) return;
        var point = _waypoints[_nextPoint];
        var posPoint = point;
        posPoint.y = _model.transform.position.y;
        Vector3 dir = posPoint - _model.transform.position;
        if (dir.magnitude < 0.2f)
        {
            if (_nextPoint + 1 < _waypoints.Count)
            {
                _nextPoint++;
                if (MyGrid.instance.IsRightPos(_waypoints[_nextPoint]))
                {

                }
            }
            else
            {
                _isFinishPath = true;
                return;
            }
        }
        _model.Move(dir.normalized);
        _model.LookDir(dir);
    }
    public bool IsFinishPath => _isFinishPath;
}
