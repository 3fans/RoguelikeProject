using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private float _timer = 1f;
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            enemyStateMachine.ChangeState(enemy.WanderState);
        }

    }
}
