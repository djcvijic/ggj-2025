using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class AggroAndFollowBehaviourOrca : EnemyBehaviour
{
    public float ChaseDuration => App.Instance.GameSettings.ChaseDurationOrca; 
    public float ChaseCooldown => App.Instance.GameSettings.ChaseCooldownOrca; 
    
    private float chaseTimer;
    private float cooldownTimer;
    private Vector2 startingPosition;
    private bool isChasing;
    private bool isOnCooldown;

    public override void Initialize(Enemy enemy)
    {
        var spriteRotator = enemy.AddComponent<SpriteRotator>();
        spriteRotator.Initialize(() => -GetMoveDirection());
        startingPosition = enemy.transform.position; // Store the starting position
    }

    public override void Execute(float deltaTime)
    {
        if (Enemy.IsDead)
            return;

        var t = Enemy.transform;
        var position = t.position;
        var diff = PlayerPositionGetter() - (Vector2)position;

        if (isChasing)
        {
            chaseTimer -= deltaTime;

            if (chaseTimer <= 0)
            {
                // Stop chasing and return to the starting position
                isChasing = false;
                isOnCooldown = true;
                cooldownTimer = ChaseCooldown;
                ReturnToStartingPosition(deltaTime);
            }
            else
            {
                // Continue chasing the player
                MoveTowards(deltaTime, diff);
            }
        }
        else if (isOnCooldown)
        {
            cooldownTimer -= deltaTime;
            if (cooldownTimer <= 0)
            {
                isOnCooldown = false;
            }
            ReturnToStartingPosition(deltaTime);
        }
        else
        {
            if (ShouldFollowPlayer(diff))
            {
                // Start chasing the player
                isChasing = true;
                chaseTimer = ChaseDuration;
            }
            else
            {
                // Return to the starting position
                ReturnToStartingPosition(deltaTime);
            }
        }
    }

    private Vector2 GetMoveDirection()
    {
        var t = Enemy.transform;
        var position = t.position;
        var diff = PlayerPositionGetter() - (Vector2)position;

        var direction = diff.normalized;
        return direction;
    }

    private bool ShouldFollowPlayer(Vector2 diff)
    {
        return diff.magnitude < Enemy.Info.AggroRange;
    }

    private void MoveTowards(float deltaTime, Vector2 diff)
    {
        var direction = diff.normalized;
        var position = Enemy.transform.position;
        Enemy.Rigidbody.MovePosition(position + deltaTime * Enemy.Info.MoveSpeed * (Vector3)direction);
    }

    private void ReturnToStartingPosition(float deltaTime)
    {
        var position = Enemy.transform.position;
        var diff = startingPosition - (Vector2)position;

        if (diff.magnitude > 0.1f) // If not close to starting position
        {
            var direction = diff.normalized;
            Enemy.Rigidbody.MovePosition(position + deltaTime * Enemy.Info.MoveSpeed * (Vector3)direction);
        }
        else
        {
            // Snap to starting position if close enough
            Enemy.Rigidbody.MovePosition(startingPosition);
        }
    }
}
