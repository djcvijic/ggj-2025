using UnityEngine;

public class PlayerMovement
{
    private Player _player;
    private InertialMovement _inertialMovement;
    private Transform _transform;

    private Vector3 Position
    {
        get => _transform.position;
        set => _transform.position = value;
    }

    public PlayerMovement(Player player)
    {
        _player = player;
        _transform = _player.transform;
        _inertialMovement = new InertialMovement();
    }

    public void UpdatePosition()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        Vector3 newPosition = _inertialMovement.CalculateNewPosition(Position, input, Time.deltaTime);

        Position = newPosition;
    }
}