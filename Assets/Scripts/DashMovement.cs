using UnityEngine;

public class DashMovement
{
    private Vector3 _dashDirection;
    private float DashSpeed => App.Instance.GameSettings.dashSpeed;
    private float DashDuration => App.Instance.GameSettings.dashDuration;
    
    private float _dashTimer;

    public bool IsDashing { get; private set; }

    public void StartDash(Vector3 currentPosition, Vector3 targetPosition)
    {
        _dashDirection = (targetPosition - currentPosition).normalized;
        IsDashing = true;
        _dashTimer = DashDuration;
    }

    public Vector3 UpdateDash(Vector3 currentPosition, float deltaTime)
    {
        if (_dashTimer > 0)
        {
            _dashTimer -= deltaTime;
            return currentPosition + _dashDirection * (DashSpeed * deltaTime);
        }
        else
        {
            IsDashing = false;
            return currentPosition;
        }
    }
}