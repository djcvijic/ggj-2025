using UnityEngine;

public class PlayerMovement
{
    private InputMovement _inputMovement;
    private DashMovement _dashMovement;
    private SpriteRotator _spriteRotator;
    
    private Transform _transform;
    private Camera _camera;
    private Rigidbody2D _rb;

    public PlayerMovement(Player player)
    {
        _camera = Camera.main;
        _transform = player.transform;
        _rb = player.GetComponent<Rigidbody2D>();
        
        _inputMovement = new InputMovement(_rb);
        _dashMovement = new DashMovement(_rb);
        
        _spriteRotator = player.GetComponent<SpriteRotator>();
        _spriteRotator.Initialize(() => _rb.velocity.normalized);
    }

    public void Update()
    {
        CheckForDash();
        _inputMovement.HandleMovementFromInput();
        _dashMovement.HandleMovementFromDash();
    }

    private void CheckForDash()
    {
        if (!Input.GetMouseButtonDown(0)) 
            return;
        
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = _camera.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;

        _dashMovement.StartDash(_transform.position, mousePosition);
    }
}