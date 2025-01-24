using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Game Settings")]
public class GameSettings : ScriptableObject
{
    public float acceleration = 30f;
    public float maxSpeed = 8f;
    public float friction = 10f;

    public float dashSpeed = 20f;
    public float dashDuration = 0.15f;
    public float dashCooldown = 2f;
}