using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Game Settings")]
public class GameSettings : ScriptableObject
{
    public float acceleration = 5f;
    public float maxSpeed = 10f;
    public float friction = 3f;
}