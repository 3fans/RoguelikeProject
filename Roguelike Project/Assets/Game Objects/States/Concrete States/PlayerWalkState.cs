using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalkState : PlayerState
{
    public PlayerWalkState(PlayerScript player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
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
        moveDirection = player.move.ReadValue<Vector2>();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.RB.velocity = new Vector2(moveDirection.x * player.moveSpeed, moveDirection.y * player.moveSpeed);
        player.animator.SetFloat("XVelocity", moveDirection.x);
        player.animator.SetFloat("YVelocity", moveDirection.y);
    }

    public override void AnimationTriggerEvent(PlayerScript.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }
}
