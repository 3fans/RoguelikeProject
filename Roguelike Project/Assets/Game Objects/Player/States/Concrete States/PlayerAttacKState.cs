using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacKState : PlayerState
{
    public PlayerAttacKState(PlayerScript player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        GameObject.Instantiate(player.projectile, player.RB.transform);

        player.abilityTimer = player.shootCooldown;
        player.StateMachine.ChangeState(player.WalkState);
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
    }

}
