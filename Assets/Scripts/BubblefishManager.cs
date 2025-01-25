﻿using UnityEngine;
using UnityEngine.Serialization;

public class BubblefishManager : MonoBehaviour
{
    [SerializeField] private BubblefishSpawner bubblefishSpawner;

    public int BubblefishPopped => bubblefishSpawner.BubblefishPopped;
    
    public void Initialize(Player player)
    {
        bubblefishSpawner.Initialize(() => player.transform.position);
    }
}