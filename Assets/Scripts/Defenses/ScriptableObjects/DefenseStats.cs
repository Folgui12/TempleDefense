using UnityEngine;

[UnityEngine.CreateAssetMenu(fileName = "DefenseStats", menuName = "Defense/Stats", order = 1)]

public class DefenseStats : ScriptableObject
{
    [field: SerializeField] public float Life { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public DefenseType Type { get; private set; }
}