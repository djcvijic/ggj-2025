using UnityEngine;

public class PlayerGrace : MonoBehaviour
{
    private GraceState _state = GraceState.NotGraced;
    private float _gracedSecondsRemaining;
    
    public bool IsGraced => _state == GraceState.Graced;
    public bool IsNotGraced => _state == GraceState.NotGraced;
    
    public float GraceDuration => App.Instance.GameSettings.gracePeriod;
    public float GraceBlinkInterval => App.Instance.GameSettings.graceBlinkInterval;

    private void Update()
    {
        UpdateGracePeriod();
    }

    public void TriggerGracePeriod()
    {
        _state = GraceState.Graced;
        _gracedSecondsRemaining = GraceDuration;
        App.Instance.EventsNotifier.NotifyGraceChanged(true);
    }

    private void UpdateGracePeriod()
    {
        print("Graced:" + IsGraced);
        
        if (!IsGraced) 
            return;
        
        _gracedSecondsRemaining -= Time.deltaTime;
        Blink();

        if (_gracedSecondsRemaining < 0)
            FinishGracePeriod();
    }

    private void Blink()
    {
        
    }

    private void FinishGracePeriod()
    {
        _state = GraceState.NotGraced;
        App.Instance.EventsNotifier.NotifyGraceChanged(false);
    }

    
    
    
    private enum GraceState
    {
        NotGraced,
        Graced,
    }
}