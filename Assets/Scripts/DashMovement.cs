using UnityEngine;

public class DashMovement
{
    private Vector3 _dashDirection;
    private Rigidbody2D _rb;

    private float DashSpeed
    {
        get
        {
            float normalizedPercentage = Mathf.Clamp01(App.Instance.BubblefishManager.BubblefishPoppedPercentage / 100f);
            float curveValue = App.Instance.GameSettings.dashSpeedCurve.Evaluate(normalizedPercentage);
            return Mathf.Lerp(App.Instance.GameSettings.minDashSpeed, App.Instance.GameSettings.maxDashSpeed, curveValue);
        }
    }

    private float DashCooldown
    {
        get
        {
            float normalizedPercentage = Mathf.Clamp01(App.Instance.BubblefishManager.BubblefishPoppedPercentage / 100f);
            float curveValue = App.Instance.GameSettings.dashCooldownCurve.Evaluate(normalizedPercentage);
            return Mathf.Lerp(App.Instance.GameSettings.minDashCooldown, App.Instance.GameSettings.maxDashCooldown, curveValue);
        }
    }

    private float DashDuration => App.Instance.GameSettings.dashDuration;

    private float _dashTimer;
    private float _cooldownTimer;

    public bool IsDashing { get; private set; }
    public bool IsOnCooldown => _cooldownTimer > 0;

    public DashMovement(Rigidbody2D rb)
    {
        _rb = rb;
    }

    public void StartDash(Vector3 currentPosition, Vector3 targetPosition)
    {
        if (IsDashing || IsOnCooldown)
            return;

        _rb.velocity = Vector3.zero;
        _dashDirection = (targetPosition - currentPosition).normalized;
        _dashTimer = DashDuration;

        IsDashing = true;
    }

    public void HandleMovementFromDash()
    {
        if (IsDashing)
        {
            if (_dashTimer > 0)
            {
                _dashTimer -= Time.deltaTime;

                Vector3 force = _dashDirection * DashSpeed;
                _rb.AddForce(force * Time.deltaTime);
            }
            else
            {
                // End the dash and start cooldown
                IsDashing = false;
                _cooldownTimer = DashCooldown;
            }
        }
        else if (IsOnCooldown)
        {
            // Decrement cooldown timer
            _cooldownTimer -= Time.deltaTime;
        }
    }

    public bool CanDash()
    {
        return !IsDashing && !IsOnCooldown;
    }
}