﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Game Settings")]
public class GameSettings : ScriptableObject
{

    public Vector3 initialCameraPosition = new Vector3(0, -11, -10);
    public float cameraFollowSpeed = 30f;
    public float cameraOffsetDistance = 8f;
    public float baseCameraDistance = 7f;
    
    public float acceleration = 30f;
    public float maxSpeed = 8f;
    public float friction = 10f;

    public float dashSpeed = 10f;
    public float dashDuration = 0.25f;
    public float dashCooldown = 2f;

    [field: SerializeField] public List<SpawnLayer> SpawnLayers { get; private set; } = new();
    [field: SerializeField] public float FollowSpeed { get; private set; } = 1f;
    [field: SerializeField] public float SecondsPuffedAfterDashEnds { get; private set; } = 1f;
}