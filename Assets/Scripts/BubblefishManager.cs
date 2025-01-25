using UnityEngine;
using UnityEngine.Serialization;

public class BubblefishManager : MonoBehaviour
{
    [SerializeField] private BubblefishSpawner bubblefishSpawner;

    public int BubblefishPopped => bubblefishSpawner.BubblefishPoppedCount;
    public int BubblefishPoppedPercentage => (int)((BubblefishPopped / (float)App.Instance.GameSettings.MaxPoppedBubblefish) * 100f);

    
    public void Initialize(Player player)
    {
        bubblefishSpawner.Initialize(() => player.transform.position);
    }
}