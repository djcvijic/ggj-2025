using UnityEngine;

public class InertialMovement
{
    private Vector3 _velocity = Vector3.zero;

    public float BaseAcceleration => App.Instance.GameSettings.acceleration;
    public float MaxSpeed => App.Instance.GameSettings.maxSpeed;
    public float Friction => App.Instance.GameSettings.acceleration;

    public Vector3 CalculateNewPosition(Vector3 currentPosition, Vector2 input, float deltaTime)
    {
        if (input != Vector2.zero)
        {
            _velocity += (Vector3)input * (BaseAcceleration * deltaTime);
        }

        if (_velocity.magnitude > 0)
        {
            float frictionAmount = Friction * deltaTime;

            if (_velocity.magnitude - frictionAmount < 0)
            {
                _velocity = Vector3.zero;
            }
            else
            {
                _velocity -= _velocity.normalized * frictionAmount;
            }
        }

        _velocity = Vector3.ClampMagnitude(_velocity, MaxSpeed);

        return currentPosition + _velocity * deltaTime;
    }
}