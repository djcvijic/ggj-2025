using System;

public class EventsNotifier
{
    private event Action<Bubblefish> _bubblefishPopped;

    public void SubscribeBubblefishPopped(Action<Bubblefish> toAdd)
        => _bubblefishPopped += toAdd;
    public void UnSubscribeBubblefishPopped(Action<Bubblefish> toRemove)
        => _bubblefishPopped -= toRemove;
    public void NotifyBubblefishPopped(Bubblefish bubblefish) 
        => _bubblefishPopped?.Invoke(bubblefish);
}