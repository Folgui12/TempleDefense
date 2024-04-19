using UnityEngine;

[UnityEngine.CreateAssetMenu(fileName = "DefenseStats", menuName = "Defense/Stats", order = 1)]

public class DefenseStats : ScriptableObject
{
    [field: SerializeField] public float _life { get; private set; }
    //field: SerializeField] public GameObject _currentObjective { get; private set; }
    //[field: SerializeField] public float _attackSpeed { get; private set; }
    [field: SerializeField] public float _attackRange { get; private set; }
    
}