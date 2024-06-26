using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Enemy/Stats", order = 0)]

public class EnemyStats : ScriptableObject
{
    [field: SerializeField] public float life { get; private set; }
    [field: SerializeField] public float travelSpeed { get; private set; }
    [field: SerializeField] public GameObject currentObjective { get; private set; }
    [field: SerializeField] public float attackSpeed { get; private set; }
    [field: SerializeField] public float attackRange { get; private set; }
    
}