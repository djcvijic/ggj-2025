﻿using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform _playerTransform;
    private Transform _cameraTransform;
    private Rigidbody2D _playerRigidbody;
    
    public static float BaseCameraDistance => App.Instance.GameSettings.baseCameraDistance;
    public float FollowSpeed => App.Instance.GameSettings.cameraFollowSpeed;
    public float OffsetDistance => App.Instance.GameSettings.cameraOffsetDistance;

    public CameraFollow Initialize(Transform playerTransform, Rigidbody2D playerRigidbody)
    {
        _cameraTransform = Camera.main.transform;
        _playerTransform = playerTransform;
        _playerRigidbody = playerRigidbody;
        
        Camera.main.orthographicSize = BaseCameraDistance;

        return this;
    }

    private void FixedUpdate()
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

    public static CameraFollow InstantiateCameraFollowObj()
    {
        var cameraFollow = new GameObject("Camera Follow").AddComponent<CameraFollow>();
        return cameraFollow;
    }
}