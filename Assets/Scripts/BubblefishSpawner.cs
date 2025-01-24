using System;
using System.Collections.Generic;
using UnityEngine;

public class BubblefishSpawner : MonoBehaviour
{
    [SerializeField] private List<Bubblefish> bubblefishPrefabs;

    public event Action<Bubblefish> BubblefishPopped;

    private void Awake()
    {
        foreach (var child in transform)
        {
            if (child is not Bubblefish bubblefish)
                continue;

            bubblefish.Popped += () => InvokeBubblefishPopped(bubblefish);
        }
    }

    private void InvokeBubblefishPopped(Bubblefish bubblefish)
    {
        BubblefishPopped?.Invoke(bubblefish);
    }

    private void OnDestroy()
    {
        BubblefishPopped = null;
    }
}
