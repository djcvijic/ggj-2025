using System;
using UnityEngine;

public abstract class EnemyBehaviour : ScriptableObject
{
    protected Enemy Enemy { get; private set; }
    protected Func<Vector2> PlayerPositionGetter { get; private set; }

    public void Initialize(Enemy enemy, Func<Vector2> playerPositionGetter)
    {
        Enemy = enemy;
        PlayerPositionGetter = playerPositionGetter;
    }

    public abstract void Execute(float deltaTime);
}