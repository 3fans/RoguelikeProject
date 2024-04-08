using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        enemy.RB.velocity = Vector3.zero;
        enemy.attackTimer = enemy.attackCooldown;
        enemy.Player.Damage(enemy.attackDamage);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        enemy.AttackTimerCountDown();
        if (enemy.attackTimer <= 0)
        {
            enemy.StateMachine.ChangeState(enemy.WanderState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
