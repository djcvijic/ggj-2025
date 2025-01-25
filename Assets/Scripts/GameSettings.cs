using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Game Settings")]
public class GameSettings : ScriptableObject
{
    [Header("Camera")]
    public Vector3 initialCameraPosition = new Vector3(0, -11, -10);
    
    public float cameraFollowSpeed = 2f;
    public float minCameraOffsetDistance = 2f;
    public float maxCameraOffsetDistance = 5f;
    
    public float minCameraDistance = 2.5f;
    public float maxCameraDistance = 12f;
    public AnimationCurve cameraDistanceCurve;
    
    [Header("Player")]
    public float acceleration = 350f;
    public float maxSpeed = 20f;
    public float friction = 4.5f;

    public float minDashSpeed = 2000f;
    public float maxDashSpeed = 4000f;
    public AnimationCurve dashSpeedCurve;
    
    public float dashDuration = 0.1f;
    public float minDashCooldown = 2f;
    public float maxDashCooldown = 4f;
    public AnimationCurve dashCooldownCurve;
    
    [field: Header("Bubblefish")]
    [field: SerializeField] public List<SpawnLayer> SpawnLayers { get; private set; } = new();
    [field: SerializeField] public float FollowSpeed { get; private set; } = 1f;
    [field: SerializeField] public float SecondsPuffedAfterDashEnds { get; private set; } = 1f;
    [field: SerializeField] public float SwarmRadiusPuffedFactor { get; private set; } = 1.5f;
    [field: SerializeField] public int MaxX { get; private set; } = 56;
    [field: SerializeField] public float PlayerSpawnY { get; private set; } = 50;
    [field: SerializeField] public int MaxBubblefishInWorld { get; private set; } = 200;
    [field: SerializeField] public int MaxPoppedBubblefish { get; private set; } = 9999;

    [field: Header("Enemies")]
    [field: SerializeField] public List<EnemySpawnInfo> EnemySpawns { get; private set; } = new();
}