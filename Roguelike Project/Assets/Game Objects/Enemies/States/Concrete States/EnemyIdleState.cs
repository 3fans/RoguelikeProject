using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Idlig");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (enemy.IsAggroed)
        {
            enemy.StateMachine.ChangeState(enemy.WanderState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
