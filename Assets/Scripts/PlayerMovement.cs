using UnityEngine;

public class PlayerMovement
{
    private InputMovement _inputMovement;
    private DashMovement _dashMovement;
    
    private Transform _transform;
    private Camera _camera;

    public PlayerMovement(Player player)
    {
        _camera = Camera.main;
        _transform = player.transform;
        
        var rb = player.GetComponent<Rigidbody2D>();
        _inputMovement = new InputMovement(rb);
        _dashMovement = new DashMovement(rb);
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