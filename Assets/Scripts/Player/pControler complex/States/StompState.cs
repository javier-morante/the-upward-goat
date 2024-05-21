using UnityEngine;

class StompState : PState
{
    public StompState(PlayerController machine, PlayerStats playerStats) : base(machine,playerStats)
    {
    }

    public override void OnEnter()
    {
        anim.SetTrigger(PlayerStates.Stomping.ToString());
    }

    public override void OnPhysicsUpdate()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
         if (inputs.inputData.move != 0)
        {
            machine.ChangeState(PlayerStates.Walking);
        }
    }

    public override void OnExit()
    {
        anim.ResetTrigger(PlayerStates.Stomping.ToString());
    }
}