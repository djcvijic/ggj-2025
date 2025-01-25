using System;
using UnityEngine;

[Serializable]
public class EnemySpawnInfo
{
    [field: SerializeField] public int Count { get; private set; } = 1;
    [field: SerializeField] public Enemy Prefab { get; private set; }
    [field: SerializeField] public int MinY { get; private set; } = -32;
    [field: SerializeField] public int MaxY { get; private set; } = 32;
    
    [field: SerializeField] public float Scale { get; private set; } = 1;
    [field: SerializeField] public float MoveSpeed { get; private set; } = 0.5f;
    [field: SerializeField] public float AggroRange { get; private set; } = 20f;
    [field: SerializeField] public int Damage { get; private set; } = 1;
    [field: SerializeField] public int MaxHealth { get; private set; } = 1;
    [field: SerializeField] public int RequiredBubblefishToDamage { get; private set; } = 20;
    [field: SerializeField] public int KillReward { get; private set; } = 5;
}