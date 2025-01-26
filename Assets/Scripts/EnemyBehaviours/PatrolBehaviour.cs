using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class PatrolBehaviour : EnemyBehaviour
{
    private float minX = -50;
    private float maxX = 50;

    public bool mirrorDirection;
    private bool movingRight = true;

    public override void Initialize(Enemy enemy)
    {
        var spriteRotator = enemy.AddComponent<SpriteRotator>();

        var left = mirrorDirection ? Vector3.left : Vector3.right;
        var right = mirrorDirection ? Vector3.right : Vector3.left;
        spriteRotator.Initialize(() => movingRight? right : left);
    }
    
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