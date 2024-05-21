using UnityEngine;

class JumpState : PState
{
    public JumpState(PlayerController machine, PlayerStats playerStats) : base(machine, playerStats)
    { 
    }

    public override void OnEnter()
    {
        anim.SetTrigger(PlayerStates.Jumping.ToString());
    }

    public override void OnPhysicsUpdate()
    {
        if ((playerStats.jumpValue >= playerStats.maxJump || !inputs.inputData.jump && playerStats.jumpValue >= 0.1f) && machine.GetIsGrounded()){

        float tempx = inputs.inputData.move * playerStats.jumpMoveSpeed;
            float tempy = playerStats.jumpValue;
            rb.velocity = new Vector2(tempx, tempy);
            playerStats.jumpValue = 0.0f;
        }

        
        if (rb.velocity.y < 0)
        {
            machine.ChangeState(PlayerStates.Falling);
        }
    }

    public override void OnExit()
    {
        anim.ResetTrigger(PlayerStates.Jumping.ToString());
    }
}