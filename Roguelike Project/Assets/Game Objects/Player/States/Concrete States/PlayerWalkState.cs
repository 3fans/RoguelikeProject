using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalkState : PlayerState
{
    const string TWalkN = "TWalkN";
    const string TWalkS = "TWalkS";
    const string TWalkE = "TWalkE";
    public PlayerWalkState(PlayerScript player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }

    Vector2 moveDirection = Vector2.zero;
    bool isAttack = false;

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
        isAttack = player.attack.IsPressed();
        player.AbilityTimerCountDown();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (isAttack && player.abilityTimer <= 0)
        {
            player.StateMachine.ChangeState(player.AttackState);
            
        }
        player.RB.velocity = new Vector2(moveDirection.x * player.moveSpeed, moveDirection.y * player.moveSpeed);
        if (moveDirection != Vector2.zero)
        {
            player.direction8 = player.VectorToDirection(moveDirection);
            player.spriteRenderer.flipX = false;
            switch (player.direction8)
            {
                case IDirection.Direction8.N:
                    player.animator.SetTrigger(TWalkN);
                    break;
                case IDirection.Direction8.S:
                    player.animator.SetTrigger(TWalkS);
                    break;
                case IDirection.Direction8.E: case IDirection.Direction8.SE: case IDirection.Direction8.NE:
                    player.animator.SetTrigger(TWalkE);
                    break;
                case IDirection.Direction8.W: case IDirection.Direction8.SW: case IDirection.Direction8.NW:
                    player.animator.SetTrigger(TWalkE);
                    player.spriteRenderer.flipX = true;
                    break;
            }
        }
    }

}
