using System;
using UnityEngine;

[Serializable]
public class EnemySpawnInfo
{
    [field: SerializeField] public int Count { get; private set; } = 1;
    [field: SerializeField] public Enemy Prefab { get; private set; }
    [field: SerializeField] public int MinY { get; private set; } = -32;
    [field: SerializeField] public int MaxY { get; private set; } = 32;
}