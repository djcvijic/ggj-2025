using UnityEngine;

public class InputMovement
{
    public float Acceleration => App.Instance.GameSettings.acceleration;
    public float MaxSpeed => App.Instance.GameSettings.maxSpeed;
    public float Friction => App.Instance.GameSettings.friction;

    private Rigidbody2D _rb;

    public InputMovement(Rigidbody2D rb)
    {
        _rb = rb;
        _rb.drag = Friction;
    }

    public void HandleMovementFromInput()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (input == Vector2.zero) return;

        Vector3 force = (Vector3)input * Acceleration;
        _rb.AddForce(force * Time.deltaTime);

        if (_rb.velocity.magnitude > MaxSpeed)
            _rb.velocity = _rb.velocity.normalized * MaxSpeed;
    }
}