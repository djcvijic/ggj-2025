﻿using UnityEngine;

public class DashMovement
{
    private Vector3 _dashDirection;
    private Rigidbody2D _rb;

    private float DashSpeed => App.Instance.GameSettings.dashSpeed;
    private float DashDuration => App.Instance.GameSettings.dashDuration;
    private float DashCooldown => App.Instance.GameSettings.dashCooldown;

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