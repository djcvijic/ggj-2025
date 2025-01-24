using UnityEngine;

public class DashMovement
{
    private Vector3 _dashDirection;
    private float _dashSpeed;
    private float _dashDuration;
    private float _dashTimer;

    public bool IsDashing { get; private set; }

    public DashMovement(float dashSpeed, float dashDuration)
    {
        _dashSpeed = dashSpeed;
        _dashDuration = dashDuration;
    }

    public void StartDash(Vector3 currentPosition, Vector3 targetPosition)
    {
        _dashDirection = (targetPosition - currentPosition).normalized;
        IsDashing = true;
        _dashTimer = _dashDuration;
    }

    public Vector3 UpdateDash(Vector3 currentPosition, float deltaTime)
    {
        if (_dashTimer > 0)
        {
            _dashTimer -= deltaTime;
            return currentPosition + _dashDirection * (_dashSpeed * deltaTime);
        }
        else
        {
            IsDashing = false;
            return currentPosition;
        }
    }
}