using UnityEngine;

public class IdleState : PState
{
    private float move ;
    private bool jump;
    public IdleState(PlayerController machine,PlayerStats playerStats) : base(machine, playerStats)
    {
    }

    public override void OnEnter()
    {
        anim.SetTrigger(PlayerStates.Idle.ToString());
    }

    public override void OnUpdate()
    {
        move = Input.GetAxisRaw("Horizontal");
        jump = Input.GetButton("Jump");
    }

    public override void OnPhysicsUpdate()
    {
        rb.velocity = new Vector2(0,rb.velocity.y);
        if (move != 0)
        {
            machine.ChangeState(PlayerStates.Walking);
        }
        else if (jump && machine.GetIsGrounded())
        {
            machine.ChangeState(PlayerStates.Charging);
        }
        else if (rb.velocity.y < 0)
        {
            machine.ChangeState(PlayerStates.Falling);
        }
    }

    public override void OnExit()
    {
        anim.ResetTrigger(PlayerStates.Idle.ToString());
    }
}