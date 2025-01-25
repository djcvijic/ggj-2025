using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Game Settings")]
public class GameSettings : ScriptableObject
{
    public float acceleration = 30f;
    public float maxSpeed = 8f;
    public float friction = 10f;

    public float dashSpeed = 10f;
    public float dashDuration = 0.25f;
    public float dashCooldown = 2f;
    
    [field: SerializeField] public float SecondsBetweenSpawns { get; private set; } = 1f;

}