using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWanderState : EnemyState
{
    const string TWalkN = "TWalkN";
    const string TWalkS = "TWalkS";
    const string TWalkE = "TWalkE";
    public EnemyWanderState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }
    Vector2 moveDirection = Vector2.zero;
    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        moveDirection = (enemy.Player.RB.position - enemy.RB.position).normalized;

        if (enemy.direction8 == enemy.VectorToDirection(moveDirection))
        {
            Vector2 a = enemy.RB.velocity.normalized;
            enemy.RB.velocity = new Vector2(a.x * enemy.moveSpeed, a.y * enemy.moveSpeed);
        }
        else
        {
        enemy.RB.velocity = new Vector2(moveDirection.x * enemy.moveSpeed, moveDirection.y * enemy.moveSpeed);
        }

        if (moveDirection != Vector2.zero)
        {
            enemy.direction8 = enemy.VectorToDirection(moveDirection);
            enemy.spriteRenderer.flipX = false;
            switch (enemy.direction8)
            {
                case IDirection.Direction8.N:
                    enemy.animator.SetTrigger(TWalkN);
                    break;
                case IDirection.Direction8.S:
                    enemy.animator.SetTrigger(TWalkS);
                    break;
                case IDirection.Direction8.E:
                case IDirection.Direction8.SE:
                case IDirection.Direction8.NE:
                    enemy.animator.SetTrigger(TWalkE);
                    break;
                case IDirection.Direction8.W:
                case IDirection.Direction8.SW:
                case IDirection.Direction8.NW:
                    enemy.animator.SetTrigger(TWalkE);
                    enemy.spriteRenderer.flipX = true;
                    break;
            }
        }
    }
}
