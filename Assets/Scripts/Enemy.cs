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
    private bool _isBoss;
    
    public void Initialize(Func<Vector2> playerPositionGetter, EnemySpawnInfo info)
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerPositionGetter = playerPositionGetter;
        _info = info;
        _remainingHealth = info.MaxHealth;
        transform.localScale *= info.Scale;
        _isBoss = GetComponent<Boss>() != null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsDead)
            return;

        var player = other.GetComponent<Player>();
        if (player != null)
            CollideWithPlayer(player);
    }

    private void CollideWithPlayer(Player player)
    {
        Debug.Log("Collided with enemy: " + gameObject.name);
        if (BubblefishCollected >= _info.RequiredBubblefishToDamage && player.IsPuffed)
        {
            _remainingHealth--;
        }
        else
        {

            if (!player.IsGraced)
            {
                App.Instance.BubblefishManager.DamageBubblefish(_info.Damage);
                player.TriggerGracePeriod();
            }
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

        if (_isBoss)
        {
            App.Instance.GameWin.TriggerWinGame();
        }
    }
}