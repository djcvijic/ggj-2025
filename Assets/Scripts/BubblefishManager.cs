using UnityEngine;

public class BubblefishManager : MonoBehaviour
{
    [SerializeField] private BubblefishSpawner bubblefishSpawner;
    private EventsNotifier _eventsNotifier;

    private int totalBubblefishesPopped = 0;
    
    public void Initialize(Player player)
    {
        bubblefishSpawner.Initialize(() => player.transform.position);
        App.Instance.EventsNotifier.SubscribeBubblefishPopped(UpdateBubblefishCounter);
    }

    private void UpdateBubblefishCounter(Bubblefish bubblefish)
    {
        totalBubblefishesPopped++;
        Debug.Log("Bubblefishes popped: " + totalBubblefishesPopped);
    }

}