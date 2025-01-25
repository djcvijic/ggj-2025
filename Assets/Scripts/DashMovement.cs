using UnityEngine;

public class DashMovement
{
    private Vector3 _dashDirection;

    private Rigidbody2D _rb;
    private float DashSpeed => App.Instance.GameSettings.dashSpeed;
    private float DashDuration => App.Instance.GameSettings.dashDuration;

    private float _dashTimer;

    public DashMovement(Rigidbody2D rb)
    {
        _rb = rb;
    }

    public bool IsDashing { get; private set; }

    public void StartDash(Vector3 currentPosition, Vector3 targetPosition)
    {
        _rb.velocity = Vector3.zero;
        _dashDirection = (targetPosition - currentPosition).normalized;
        _dashTimer = DashDuration;

        IsDashing = true;
    }

    public void HandleMovementFromDash()
    {
        if (_dashTimer > 0)
        {
            _dashTimer -= Time.deltaTime;

            Vector3 force = _dashDirection * DashSpeed;
            _rb.AddForce(force * Time.deltaTime);

            return;
        }

        IsDashing = false;
    }
}