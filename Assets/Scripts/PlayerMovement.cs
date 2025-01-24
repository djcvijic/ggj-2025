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
        _dashMovement = new DashMovement();
    }

    public void Update()
    {
        CheckForDash();
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        Vector3  newPosition = _dashMovement.IsDashing ? 
            _dashMovement.CalculateNewPosition(Position, Time.deltaTime) : 
            _inertialMovement.CalculateNewPosition(Position, Time.deltaTime);

        Position = newPosition;
    }

    private void CheckForDash()
    {
        if (!Input.GetMouseButtonDown(0)) 
            return;
        
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = _camera.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;

        _inertialMovement.StopMovement();
        _dashMovement.StartDash(Position, mousePosition);
    }
}