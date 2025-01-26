using System;
using UnityEngine;
using UnityEngine.Serialization;

public class BubblefishManager : MonoBehaviour
{
    [SerializeField] private BubblefishSpawner bubblefishSpawner;

    public int BubblefishPopped => bubblefishSpawner.BubblefishPoppedCount;
    public int BubblefishPoppedPercentage => (int)(BubblefishPopped / (float)MaxPoppedBubblefish * 100f);

    private static int MaxPoppedBubblefish => Math.Min(
        App.Instance.GameSettings.MaxPoppedBubblefish,
        App.Instance.GameSettings.MaxBubblefishInWorld);


    public void Initialize(Player player)
    {
        bubblefishSpawner.Initialize(() => player.transform.position);
    }

    public void RewardBubblefish(int number)
    {
        bubblefishSpawner.PopFish(number);
    }
    
    public void DamageBubblefish(int number)
    {
        bubblefishSpawner.DamageFish(number);
    }
}