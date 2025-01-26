using System;

public class EventsNotifier
{
    public event Action<Bubblefish> BubblefishPopped;
    public event Action<bool> PuffednessChanged;

    public event Action<int> PlayerDamaged; 

    public void NotifyBubblefishPopped(Bubblefish bubblefish) 
        => BubblefishPopped?.Invoke(bubblefish);

    public void NotifyPuffednessChanged(bool value)
        => PuffednessChanged?.Invoke(value);
    
    public void NotifyPlayerDamaged(int damage)
        => PlayerDamaged?.Invoke(damage);
}