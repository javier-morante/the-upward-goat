using UnityEngine;

public class WalkState : PState
{

    private float move ;
    private bool jump;

    public WalkState(PlayerController machine , PlayerStats playerStats) : base(machine, playerStats)
    {
    }

    public override void OnUpdate()
    {
        move = Input.GetAxisRaw("Horizontal");
        jump = Input.GetButton("Jump");
    }

    public override void OnEnter()
    {
        anim.SetTrigger(PlayerStates.Walking.ToString());
    }

    public override void OnPhysicsUpdate()
    {

        rb.velocity = new Vector2(move * playerStats.moveSpeed, rb.velocity.y);
    
        machine.ChangeDirection();

        if (inputs.inputData.move == 0)
        {
            machine.ChangeState(PlayerStates.Idle);
        }
        else if (jump && this.machine.GetIsGrounded())
        {
            machine.ChangeState(PlayerStates.Charging);

        }
        else if (rb.velocity.y < 0)
        {
            machine.ChangeState(PlayerStates.Falling);
        }


    }

    public override void OnExit() {
        anim.ResetTrigger(PlayerStates.Walking.ToString());
    }

    
}