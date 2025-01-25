using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform _playerTransform;
    private Transform _cameraTransform;
    private Rigidbody2D _playerRigidbody;

    public float FollowSpeed => App.Instance.GameSettings.cameraFollowSpeed;
    public float OffsetDistance => App.Instance.GameSettings.cameraOffsetDistance;

    private void Update()
    {
        HandleCameraMovement();
    }

    private void HandleCameraMovement()
    {
        Vector2 playerVelocity = _playerRigidbody.velocity;
        Vector3 offset = Vector3.zero;

        if (playerVelocity.magnitude > 0.1f) 
        {
            offset = (Vector3)playerVelocity.normalized * OffsetDistance;
        }

        Vector3 targetPosition = _playerTransform.position + offset;
        targetPosition.z = _cameraTransform.position.z; 

        _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, targetPosition, FollowSpeed * Time.deltaTime);
    }

    public static CameraFollow InstantiateCameraFollowObj(Transform playerTransform, Rigidbody2D playerRigidbody)
    {
        var cameraFollow = new GameObject("Camera Follow").AddComponent<CameraFollow>();
        
        cameraFollow._cameraTransform = Camera.main.transform;
        cameraFollow._playerTransform = playerTransform;
        cameraFollow._playerRigidbody = playerRigidbody;

        return cameraFollow;
    }
}