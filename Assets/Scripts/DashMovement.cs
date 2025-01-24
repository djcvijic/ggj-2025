using UnityEngine;

public class DashMovement
{
    private Vector3 _dashDirection;
    private Vector3 _targetPosition;
    private float DashSpeed => App.Instance.GameSettings.dashSpeed;
    private float DashDuration => App.Instance.GameSettings.dashDuration;
    private float DecelerationFactor => App.Instance.GameSettings.dashDeceleration;

    private float _dashTimer;
    private float _currentSpeed;

    public bool IsDashing { get; private set; }

    public void StartDash(Vector3 currentPosition, Vector3 targetPosition)
    {
        _dashDirection = (targetPosition - currentPosition).normalized;
        _targetPosition = targetPosition;
        IsDashing = true;
        _dashTimer = DashDuration;
        _currentSpeed = DashSpeed;
    }

    public Vector3 CalculateNewPosition(Vector3 currentPosition, float deltaTime)
    {
        if (_dashTimer > 0)
        {
            _dashTimer -= deltaTime;

            float deceleration = DecelerationFactor * (1 - (_dashTimer / DashDuration));
            float adjustedSpeed = Mathf.Max(0, _currentSpeed - deceleration);

            _currentSpeed = adjustedSpeed;

            return currentPosition + _dashDirection * (_currentSpeed * deltaTime);
        }
        else
        {
            IsDashing = false;
            return currentPosition;
        }
    }
}