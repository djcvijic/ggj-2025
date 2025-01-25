using System;
using UnityEngine;

[Serializable]
public class SpawnLayer
{
    [field: SerializeField] public int MinY { get; private set; } = -32;
    [field: SerializeField] public int MaxY { get; private set; } = 32;
    [field: SerializeField] public float SecondsBetweenSpawns { get; private set; } = 1f;
}