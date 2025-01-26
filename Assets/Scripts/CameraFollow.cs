using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform _playerTransform;
    private Transform _cameraTransform;
    private Rigidbody2D _playerRigidbody;
    private Camera _camera;

    private float CameraDistance
    {
        get
        {
            float normalizedPercentage = Mathf.Clamp01(App.Instance.BubblefishManager.BubblefishPoppedPercentage / 100f);
            float curveValue = App.Instance.GameSettings.cameraDistanceCurve.Evaluate(normalizedPercentage);
            return Mathf.Lerp(App.Instance.GameSettings.minCameraDistance, App.Instance.GameSettings.maxCameraDistance, curveValue);
        }
    }
    public static Vector3 InitialCameraPosition => App.Instance.GameSettings.initialCameraPosition;
    public float FollowSpeed => App.Instance.GameSettings.cameraFollowSpeed;
    public float MaxPlayerSpeed => App.Instance.GameSettings.maxSpeed; 
    public float MinOffset => App.Instance.GameSettings.minCameraOffsetDistance; // Offset when stationary
    public float MaxOffset => App.Instance.GameSettings.maxCameraOffsetDistance; // Offset at max speed

    public CameraFollow Initialize(Transform playerTransform, Rigidbody2D playerRigidbody)
    {
        _camera = Camera.main;
        _cameraTransform = _camera.transform;
        _playerTransform = playerTransform;
        _playerRigidbody = playerRigidbody;

        _camera.orthographicSize = CameraDistance;
        _camera.transform.position = InitialCameraPosition;

        App.Instance.EventsNotifier.BubblefishPopped += OnBubblefishPopped;
        App.Instance.EventsNotifier.BubblefishDied += OnBubblefishDied;

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

    private void OnBubblefishPopped(Bubblefish bubblefish)
    {
        _camera.orthographicSize = CameraDistance;
    }

    private void OnBubblefishDied(Bubblefish bubblefish)
    {
        _camera.orthographicSize = CameraDistance;
    }
    
    public static CameraFollow InstantiateCameraFollowObj()
    {
        var cameraFollow = new GameObject("Camera Follow").AddComponent<CameraFollow>();
        return cameraFollow;
    }
}
