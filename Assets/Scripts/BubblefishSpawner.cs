using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class BubblefishSpawner : MonoBehaviour
{
    [SerializeField] private List<Bubblefish> bubblefishPrefabs;
    private int MaxX => App.Instance.GameSettings.MaxX;

    private readonly Random _random = new();

    private Func<Vector2> _playerPositionGetter;

    private readonly List<Bubblefish> _bubblefishList = new();

    private IEnumerable<Bubblefish> BubblefishPopped => _bubblefishList.Where(x => x.IsPopped);

    public int BubblefishPoppedCount => _bubblefishList.Count(x => x.IsPopped);

    public void Initialize(Func<Vector2> playerPositionGetter)
    {
        _playerPositionGetter = playerPositionGetter;
        
        RegisterExistingChildren();
        StartSpawning();

        App.Instance.EventsNotifier.BubblefishPopped += OnBubblefishPopped;
        App.Instance.EventsNotifier.PuffednessChanged += OnPuffednessChanged;
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
        _bubblefishList.Add(bubblefish);
        Debug.Log($"Total bubblefish in the world: {_bubblefishList.Count}");
    }

    private void StartSpawning()
    {
        foreach (var layer in App.Instance.GameSettings.SpawnLayers)
        {
            StartCoroutine(SpawnPeriodically(layer));
        }
    }

    private IEnumerator SpawnPeriodically(SpawnLayer layer)
    {
        while (true)
        {
            yield return new WaitForSeconds(layer.SecondsBetweenSpawns);
            SpawnBubbleFish(layer.MinY, layer.MaxY);
        }
    }

    private void SpawnBubbleFish(int minY, int maxY)
    {
        var prefab = bubblefishPrefabs.GetRandomElement();
        var randomPosition = new Vector3(_random.Next(-MaxX, MaxX), _random.Next(minY, maxY), 0);
        var bubblefish = Instantiate(prefab, randomPosition, Quaternion.identity, transform);
        InitializeBubblefish(bubblefish);
    }

    private void OnBubblefishPopped(Bubblefish bubblefish)
    {
        Debug.Log($"Total bubblefish popped: {BubblefishPoppedCount}");
    }

    private void OnPuffednessChanged(bool value)
    {
        foreach (var bubblefish in BubblefishPopped)
        {
            bubblefish.SetPuffed(value);
        }
    }
}