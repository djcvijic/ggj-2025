using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Func<Vector2> _playerPositionGetter;

    private bool IsDead => throw new NotImplementedException();

    public void Initialize(Func<Vector2> playerPositionGetter)
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerPositionGetter = playerPositionGetter;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsDead)
            return;

        var player = other.GetComponent<Player>();
        if (player != null && player.IsPuffed)
            TakeAHit();
    }

    private void TakeAHit()
    {
        throw new NotImplementedException();
    }

    private void FixedUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        if (IsDead)
            return;

        var t = transform;
        var position = t.position;
        var diff = _playerPositionGetter() - (Vector2)position;
        if (!ShouldFollowPlayer(diff))
            return;

        _rigidbody.MovePosition(position + Time.fixedDeltaTime * App.Instance.GameSettings.EnemyFollowSpeed * (Vector3)diff);
    }

    private bool ShouldFollowPlayer(Vector2 diff)
    {
        throw new NotImplementedException();
    }
}