using System;

public class BubblefishPoppedEvent
{
    private event Action<Bubblefish> _bubblefishPopped;

    public void AddEvent(Action<Bubblefish> toAdd)
    {
        _bubblefishPopped += toAdd;
    }

    public void RemoveEvent(Action<Bubblefish> toRemove)
    {
        _bubblefishPopped -= toRemove;
    }

    public void OnBubblefishPopped(Bubblefish bubblefish)
    {
        _bubblefishPopped?.Invoke(bubblefish);
    }
}