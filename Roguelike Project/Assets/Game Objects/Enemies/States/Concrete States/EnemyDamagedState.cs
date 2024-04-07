using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamagedState : EnemyState
{
    float delayTime = 0;
    float speedPenalty = 0.5f;
    public EnemyDamagedState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        delayTime = 1f;
        enemy.RB.velocity = new Vector2 (enemy.RB.velocity.x * speedPenalty, enemy.RB.velocity.y * speedPenalty);
        
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        delayTime -= Time.deltaTime;
        if (delayTime < 0 )
        {
            enemy.StateMachine.ChangeState(enemy.WanderState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
