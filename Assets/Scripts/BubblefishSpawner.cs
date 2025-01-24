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

    private readonly Random random = new();

    private void Awake()
    {
        RegisterExistingChildren();
        StartSpawning();
    }

    private void RegisterExistingChildren()
    {
        foreach (var child in transform)
        {
            if (child is Bubblefish bubblefish)
                SubscribeToBubblefish(bubblefish);
        }
    }

    private void SubscribeToBubblefish(Bubblefish bubblefish)
    {
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
        yield return waitForSeconds;
        SpawnBubbleFish();
    }

    private void SpawnBubbleFish()
    {
        var prefab = bubblefishPrefabs.GetRandomElement();
        var randomPosition = new Vector3(random.Next(-maxX, maxX), random.Next(-maxY, maxY), 0);
        var bubblefish = Instantiate(prefab, randomPosition, Quaternion.identity, transform);
        SubscribeToBubblefish(bubblefish);
    }

    private void OnDestroy()
    {
        BubblefishPopped = null;
    }
}
