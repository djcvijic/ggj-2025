using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BubblefishSpawner : MonoBehaviour
{
    [SerializeField] private List<Bubblefish> bubblefishPrefabs;
    [SerializeField] private int maxX = 28;
    [SerializeField] private int maxY = 32;

    private readonly Random _random = new();

    private Func<Vector2> _playerPositionGetter;

    public void Initialize(Func<Vector2> playerPositionGetter)
    {
        _playerPositionGetter = playerPositionGetter;
        
        RegisterExistingChildren();
        StartSpawning();
    }

    private void RegisterExistingChildren()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent(out Bubblefish bubblefish))
                InitializeBubblefish(bubblefish);
        }
    }

    private void InitializeBubblefish(Bubblefish bubblefish)
    {
        bubblefish.Initialize(_playerPositionGetter);
    }

    private void StartSpawning()
    {
        StartCoroutine(SpawnPeriodically());
    }

    private IEnumerator SpawnPeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(App.Instance.GameSettings.SecondsBetweenSpawns);
            SpawnBubbleFish();
        }
    }

    private void SpawnBubbleFish()
    {
        var prefab = bubblefishPrefabs.GetRandomElement();
        var randomPosition = new Vector3(_random.Next(-maxX, maxX), _random.Next(-maxY, maxY), 0);
        var bubblefish = Instantiate(prefab, randomPosition, Quaternion.identity, transform);
        InitializeBubblefish(bubblefish);
    }
}