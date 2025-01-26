using UnityEngine;

[CreateAssetMenu]
public class PatrolBehaviour : EnemyBehaviour
{
    private float minX = -50;
    private float maxX = 50;

    public bool mirrorDirection;
    private bool movingRight = true;

    public override void Execute(float deltaTime)
    {
        if (Enemy.IsDead)
            return;

        var position = Enemy.transform.position;
        var moveSpeed = Enemy.Info.MoveSpeed;

        if (movingRight)
        {
            position.x += moveSpeed * deltaTime;
            if (position.x >= maxX)
            {
                position.x = maxX;
                movingRight = false;
            }
        }
        else
        {
            position.x -= moveSpeed * deltaTime;
            if (position.x <= minX)
            {
                position.x = minX;
                movingRight = true;
            }
        }

        Enemy.Rigidbody.MovePosition(position);
    }
}