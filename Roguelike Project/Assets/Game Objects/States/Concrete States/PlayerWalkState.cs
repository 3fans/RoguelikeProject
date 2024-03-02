using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
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
        if (moveDirection != Vector2.zero )
        {
            GetDirection(moveDirection.x, moveDirection.y);
            if (player.PlayerDirection == PlayerScript.PlayerDirectionEnum.N)
            {
                AnimationTriggerEvent(PlayerScript.AnimationTriggerType.WalkNorth);
                Debug.Log("north");
            }

            else if (player.PlayerDirection == PlayerScript.PlayerDirectionEnum.S) {
                AnimationTriggerEvent(PlayerScript.AnimationTriggerType.WalkSouth);
                Debug.Log("south");
            }
            
        }
        
    }

    public override void AnimationTriggerEvent(PlayerScript.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    private void GetDirection(float x, float y)
    {
        //Gets the angle that the player is facing
        float rawAngle = (Mathf.Atan(y / x) * 180 / Mathf.PI);
        if (x < 0)
        {
            rawAngle += 180;
        }
        else if (rawAngle < 0)
        {
            rawAngle = 360 + rawAngle;
        }

        int angleSeperation = 45;
        int angleOffset = angleSeperation / 2;

        #region Determine direction based on angle and set the cardinal direction
        if ((rawAngle >= -angleOffset) && (rawAngle < angleOffset))
        {
            player.PlayerDirection = PlayerScript.PlayerDirectionEnum.E;
        }
        else if ((rawAngle >= angleOffset) && (rawAngle < angleOffset + angleSeperation))
        {
            player.PlayerDirection = PlayerScript.PlayerDirectionEnum.NE;
        }
        else if ((rawAngle >= angleOffset + angleSeperation) && (rawAngle < angleOffset + 2 * angleSeperation))
        {
            player.PlayerDirection = PlayerScript.PlayerDirectionEnum.N;
        }
        else if ((rawAngle >= angleOffset + 2 * angleSeperation) && (rawAngle < angleOffset + 3 * angleSeperation))
        {
            player.PlayerDirection = PlayerScript.PlayerDirectionEnum.NW;
        }
        else if ((rawAngle >= angleOffset + 3 * angleSeperation) && (rawAngle < angleOffset + 4 * angleSeperation))
        {
            player.PlayerDirection = PlayerScript.PlayerDirectionEnum.W;
        }
        else if ((rawAngle >= angleOffset + 4 * angleSeperation) && (rawAngle < angleOffset + 5 * angleSeperation))
        {
            player.PlayerDirection = PlayerScript.PlayerDirectionEnum.SW;
        }
        else if ((rawAngle >= angleOffset + 5 * angleSeperation) && (rawAngle < angleOffset + 6 * angleSeperation))
        {
            player.PlayerDirection = PlayerScript.PlayerDirectionEnum.S;
        }
        else if ((rawAngle >= angleOffset + 6 * angleSeperation) && (rawAngle < angleOffset + 7 * angleSeperation))
        {
            player.PlayerDirection = PlayerScript.PlayerDirectionEnum.SE;
        }
        #endregion

    }
}
