using UnityEngine;

public class PlayerMovement
{
    private Player _player;
    
    private InputMovement _inputMovement;
    private DashMovement _dashMovement;
    
    private Transform _transform;
    private Camera _camera;

    private Rigidbody2D _rb;

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
        _rb = _player.GetComponent<Rigidbody2D>();

        _inputMovement = new InputMovement(_rb);
        _dashMovement = new DashMovement(_rb);
    }

    public void Update()
    {
        _inputMovement.HandleMovementFromInput();
        
        // CheckForDash();
        // UpdatePosition();
    }

    private void UpdatePosition()
    {
        Vector3  newPosition = _dashMovement.IsDashing ? 
            _dashMovement.CalculateNewPosition(Position, Time.deltaTime) : 
            _inputMovement.HandleMovementFromInput();

        Position = newPosition;
    }

    private void CheckForDash()
    {
        if (!Input.GetMouseButtonDown(0)) 
            return;
        
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = _camera.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;

        _inputMovement.StopMovement();
        _dashMovement.StartDash(Position, mousePosition);
    }

    public void HandleMovement()
    {
        _inputMovement.HandleMovementFromInput();
    }
}