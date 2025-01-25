using System;

public class EventsNotifier
{
    public event Action<Bubblefish> BubblefishPopped;

    public void NotifyBubblefishPopped(Bubblefish bubblefish) 
        => BubblefishPopped?.Invoke(bubblefish);
}