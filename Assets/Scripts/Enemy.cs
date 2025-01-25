using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Func<Vector2> _playerPositionGetter;
    private EnemySpawnInfo _info;

    private int _remainingHealth;

    private bool IsDead => _remainingHealth <= 0;
    private int BubblefishCollected => App.Instance.BubblefishManager.BubblefishPopped;
    
    public void Initialize(Func<Vector2> playerPositionGetter, EnemySpawnInfo info)
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerPositionGetter = playerPositionGetter;
        _info = info;
        _remainingHealth = info.MaxHealth;
        transform.localScale *= info.Scale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsDead)
            return;

        var player = other.GetComponent<Player>();
        if (player != null && player.IsPuffed)
            CollideWithPlayer();
    }

    private void CollideWithPlayer()
    {
        Debug.Log("Collided with enemy: " + gameObject.name);
        if (BubblefishCollected >= _info.RequiredBubblefishToDamage)
        {
            _remainingHealth--;
        }
        else
        {
            // TODO - Destroy a certain number of bubble fishes the player collected - enemy damage can be defined in game settings
        }

        if (_remainingHealth <= 0)
        {
            DestroyEnemy();
        }
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

        var direction = diff.normalized;
        _rigidbody.MovePosition(position + Time.fixedDeltaTime * _info.MoveSpeed * (Vector3)direction);
    }

    private bool ShouldFollowPlayer(Vector2 diff)
    {
        return diff.magnitude < _info.AggroRange;
    }

    private void DestroyEnemy()
    {
        gameObject.SetActive(false);
        App.Instance.BubblefishManager.RewardBubblefish(_info.KillReward);
    }
}