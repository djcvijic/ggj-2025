using System;
using System.Collections.Generic;
using System.Linq;
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
        var existingCount = FindObjectsByType<Enemy>(FindObjectsInactive.Include, FindObjectsSortMode.None)
            .Count(x => x.Info == info && !x.IsDead);
        for (var i = 0; i < info.Count - existingCount; i++)
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

    public bool EnemyAlive(Func<Enemy, bool> predicate)
    {
        return _enemyList.Exists(x => !x.IsDead && predicate(x));
    }

    public void EnemyDied(EnemySpawnInfo info)
    {
        _enemyList.RemoveAll(x => x.IsDead);
        SpawnEnemies(info);
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.K))
        {
            foreach (var enemy in _enemyList.ToList())
            {
                enemy.Kill();
            }
        }
#endif
    }
}
