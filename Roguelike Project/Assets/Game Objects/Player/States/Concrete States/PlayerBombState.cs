using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBombState : PlayerState
{
    public PlayerBombState(PlayerScript player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        GameObject.Instantiate(player.bombObject, player.transform);

        player.bombTimer = player.bombCooldown;
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
