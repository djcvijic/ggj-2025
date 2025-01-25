using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemyManager : MonoBehaviour
{
    private int MaxX => App.Instance.GameSettings.MaxX;

    private readonly Random _random = new();

    private Func<Vector2> _playerPositionGetter;

    private readonly List<Enemy> _enemyList = new();

    public void Initialize(Func<Vector2> playerPositionGetter)
    {
        _playerPositionGetter = playerPositionGetter;
        StartSpawning();
    }

    private void StartSpawning()
    {
        foreach (var info in App.Instance.GameSettings.EnemySpawns)
        {
            SpawnEnemies(info);
        }
    }

    private void SpawnEnemies(EnemySpawnInfo info)
    {
        for (var i = 0; i < info.Count; i++)
        {
            var prefab = info.Prefab;
            var randomPosition = new Vector3(_random.Next(-MaxX, MaxX), _random.Next(info.MinY, info.MaxY), 0);
            var enemy = Instantiate(prefab, randomPosition, Quaternion.identity, transform);
            InitializeEnemy(enemy, info);
        }
    }

    private void InitializeEnemy(Enemy enemy, EnemySpawnInfo info)
    {
        enemy.Initialize(_playerPositionGetter, info);
        _enemyList.Add(enemy);
    }
}
