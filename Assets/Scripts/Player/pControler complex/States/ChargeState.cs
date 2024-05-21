using UnityEngine;

class ChargeState : PState
{
    public ChargeState(PlayerController machine,PlayerStats playerStats) : base(machine, playerStats)
    {
    }

    public override void OnEnter()
    {
        anim.SetTrigger(PlayerStates.Charging.ToString());
    }

    public override void OnPhysicsUpdate()
    {

        rb.velocity = new Vector2(0f, rb.velocity.y);
        playerStats.jumpValue += playerStats.jumpPerF;
        machine.ChangeDirection();

        if ((playerStats.jumpValue >= playerStats.maxJump || !inputs.inputData.jump && playerStats.jumpValue >= 0.1f) && machine.GetIsGrounded())
        {
            machine.ChangeState(PlayerStates.Jumping);
        }
    }

    public override void OnExit() 
    {
        anim.ResetTrigger(PlayerStates.Charging.ToString());
    }

}