using UnityEngine;
using UnityEngine.Serialization;

public class BubblefishManager : MonoBehaviour
{
    [SerializeField] private BubblefishSpawner bubblefishSpawner;
    private EventsNotifier _eventsNotifier;

    public int BubblefishPopped => bubblefishSpawner.BubblefishPopped;
    
    public void Initialize(Player player)
    {
        bubblefishSpawner.Initialize(() => player.transform.position);
    }
}