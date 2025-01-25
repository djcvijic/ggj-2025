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

    public event Action<Bubblefish> BubblefishPopped;

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
        bubblefish.Popped += () => InvokeBubblefishPopped(bubblefish);
    }

    private void InvokeBubblefishPopped(Bubblefish bubblefish)
    {
        BubblefishPopped?.Invoke(bubblefish);
    }

    private void StartSpawning()
    {
        var waitForSeconds = new WaitForSeconds(App.Instance.GameSettings.SecondsBetweenSpawns);
        StartCoroutine(SpawnPeriodically(waitForSeconds));
    }

    private IEnumerator SpawnPeriodically(WaitForSeconds waitForSeconds)
    {
        while (true)
        {
            yield return waitForSeconds;
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

    private void OnDestroy()
    {
        BubblefishPopped = null;
    }
}
