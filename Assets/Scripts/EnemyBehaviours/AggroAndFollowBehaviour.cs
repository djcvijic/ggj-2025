using UnityEngine;

[CreateAssetMenu]
public class AggroAndFollowBehaviour : EnemyBehaviour
{
    public override void Execute(float deltaTime)
    {
        if (Enemy.IsDead)
            return;

        var t = Enemy.transform;
        var position = t.position;
        var diff = PlayerPositionGetter() - (Vector2)position;
        if (!ShouldFollowPlayer(diff))
            return;

        var direction = diff.normalized;
        Enemy.Rigidbody.MovePosition(position + deltaTime * Enemy.Info.MoveSpeed * (Vector3)direction);
    }

    private bool ShouldFollowPlayer(Vector2 diff)
    {
        return diff.magnitude < Enemy.Info.AggroRange;
    }
}