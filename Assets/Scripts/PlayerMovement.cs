using UnityEngine;

public class PlayerMovement
{
    private Player _player;
    private InertialMovement _inertialMovement;
    private DashMovement _dashMovement;
    private Transform _transform;
    private Camera _camera;

    private Vector3 Position
    {
        get => _transform.position;
        set => _transform.position = value;
    }

    public PlayerMovement(Player player)
    {
        _camera = Camera.main;
        _player = player;
        _transform = _player.transform;
        _inertialMovement = new InertialMovement();
        _dashMovement = new DashMovement(20f, 0.2f);
    }

    public void UpdatePosition()
    {
        if (_dashMovement.IsDashing)
        {
            Position = _dashMovement.UpdateDash(Position, Time.deltaTime);
        }
        else
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            Vector3 newPosition = _inertialMovement.CalculateNewPosition(Position, input, Time.deltaTime);
            Position = newPosition;
        }
    }

    public void HandleDash()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = _camera.ScreenToWorldPoint(mousePosition);
            mousePosition.z = 0;

            _inertialMovement.StopMovement();
            _dashMovement.StartDash(Position, mousePosition);
        }
    }
}