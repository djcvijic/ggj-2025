using UnityEngine;
using UnityEngine.Serialization;

public class BubblefishManager : MonoBehaviour
{
    [SerializeField] private BubblefishSpawner bubblefishSpawner;

    public int BubblefishPopped => bubblefishSpawner.BubblefishPoppedCount;
    
    public void Initialize(Player player)
    {
        bubblefishSpawner.Initialize(() => player.transform.position);
    }
}