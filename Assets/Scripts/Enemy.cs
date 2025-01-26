using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public Rigidbody2D Rigidbody { get; private set; }
    public EnemySpawnInfo Info { get; private set; }

    private int _remainingHealth;

    public bool IsDead => _remainingHealth <= 0;
    private int BubblefishCollected => App.Instance.BubblefishManager.BubblefishPopped;
    public bool IsBoss { get; private set; }

    private EnemyBehaviour _behaviour;
    private HitRegistrationEffect _hitRegistrationEffect;

    public void Initialize(Func<Vector2> playerPositionGetter, EnemySpawnInfo info)
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Info = info;
        _remainingHealth = info.MaxHealth;
        transform.localScale *= info.Scale;
        _behaviour = Instantiate(Info.Behaviour);
        _behaviour.Initialize(this, playerPositionGetter);
        IsBoss = _behaviour is BossBehaviour;
        _hitRegistrationEffect = gameObject.AddComponent<HitRegistrationEffect>();
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
        if (BubblefishCollected >= Info.RequiredBubblefishToDamage && player.IsPuffed)
        {
            _remainingHealth--;
            _hitRegistrationEffect.ToggleColor();
            if (!IsDead)
                App.Instance.AudioManager.DamageEnemy();
            else if (IsBoss)
                App.Instance.AudioManager.BossKill();
            else
                App.Instance.AudioManager.EnemyKill();
        }
        else
        {
            if (!player.IsGraced)
            {
                App.Instance.BubblefishManager.DamageBubblefish(Info.Damage);
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
        if (App.Instance.ShouldPauseAllMovement)
            return;

        _behaviour.Execute(Time.fixedDeltaTime);
    }

    private void DestroyEnemy()
    {
        gameObject.SetActive(false);
        App.Instance.BubblefishManager.RewardBubblefish(Info.KillReward);
    }
}