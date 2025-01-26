using System;

public class EventsNotifier
{
    public event Action<Bubblefish> BubblefishPopped;
    public event Action<Bubblefish> BubblefishDied;
    public event Action<bool> PuffednessChanged;
    public event Action<bool> GraceChanged;

    public event Action<int> PlayerDamaged;

    public event Action PassedFishSizeRequirement;

    public void NotifyBubblefishPopped(Bubblefish bubblefish) 
        => BubblefishPopped?.Invoke(bubblefish);
    public void NotifyBubblefishDied(Bubblefish bubblefish) 
        => BubblefishDied?.Invoke(bubblefish);
    
    public void NotifyPuffednessChanged(bool value)
        => PuffednessChanged?.Invoke(value);
    public void NotifyGraceChanged(bool value)
        => GraceChanged?.Invoke(value);
    public void NotifyPlayerDamaged(int damage)
        => PlayerDamaged?.Invoke(damage);
    
    public void NotifyPassedFishSizeRequirement()
        => PassedFishSizeRequirement?.Invoke();
}