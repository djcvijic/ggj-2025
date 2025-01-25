using System;
using UnityEngine;

public class SpriteRotator : MonoBehaviour
{
    private Func<Vector2> _getDirection;
    private Vector3 _initialScale;

    public void Initialize(Func<Vector2> getDirection)
    {
        _getDirection = getDirection;
    }

    private void Awake()
    {
        _initialScale = transform.localScale;
    }

    private void Update()
    {
        UpdateDirection();
    }

    private void UpdateDirection()
    {
        if (_getDirection == null)
            return;

        var direction = _getDirection();
        if (direction.magnitude == 0)
            return;

        direction = direction.normalized;
        var angle = Vector2.SignedAngle(Vector2.right, direction);
        var t = transform;
        if (angle is < -90f or > 90f)
        {
            t.localScale = new Vector3(-_initialScale.x, _initialScale.y, _initialScale.z);
            angle = angle switch
            {
                >= 90 => angle - 180,
                <= -90 => angle + 180,
                _ => angle
            };
        }
        else
        {
            t.localScale = _initialScale;
        }

        t.localRotation = Quaternion.Euler(0f, 0f, angle);
    }
}