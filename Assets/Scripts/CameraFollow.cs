using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform _playerTransform;
    private Transform _cameraTransform;
    private Rigidbody2D _playerRigidbody;

    public static float BaseCameraDistance => App.Instance.GameSettings.baseCameraDistance;
    public static Vector3 InitialCameraPosition => App.Instance.GameSettings.initialCameraPosition;
    public float FollowSpeed => App.Instance.GameSettings.cameraFollowSpeed;
    public float MaxPlayerSpeed => App.Instance.GameSettings.maxSpeed; 
    public float MinOffset => App.Instance.GameSettings.minCameraOffsetDistance; // Offset when stationary
    public float MaxOffset => App.Instance.GameSettings.maxCameraOffsetDistance; // Offset at max speed

    public CameraFollow Initialize(Transform playerTransform, Rigidbody2D playerRigidbody)
    {
        _cameraTransform = Camera.main.transform;
        _playerTransform = playerTransform;
        _playerRigidbody = playerRigidbody;

        Camera.main.orthographicSize = BaseCameraDistance;
        Camera.main.transform.position = InitialCameraPosition;

        return this;
    }

    private void FixedUpdate()
    {
        HandleCameraMovement();
    }

    private void HandleCameraMovement()
    {
        float playerSpeed = _playerRigidbody.velocity.magnitude;
        float speedRatio = Mathf.Clamp01(playerSpeed / MaxPlayerSpeed);

        float offsetDistance = Mathf.Lerp(MinOffset, MaxOffset, speedRatio);
        Vector3 offset = Vector3.zero;

        if (playerSpeed > 0.1f)
        {
            offset = (Vector3)_playerRigidbody.velocity.normalized * offsetDistance;
        }

        Vector3 targetPosition = _playerTransform.position + offset;
        targetPosition.z = _cameraTransform.position.z; // Preserve Z-axis

        _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, targetPosition, FollowSpeed * Time.deltaTime);
    }

    public static CameraFollow InstantiateCameraFollowObj()
    {
        var cameraFollow = new GameObject("Camera Follow").AddComponent<CameraFollow>();
        return cameraFollow;
    }
}
