using UnityEngine;

class BounceState : PState
{
    public BounceState(PlayerController machine,  PlayerStats playerStats) : base(machine, playerStats)
    {
        
    }

    public override void OnEnter()
    {
        anim.SetTrigger(PlayerStates.Bouncing.ToString());
    }

    public override void OnPhysicsUpdate()
    {
       machine.ApplyFallVelocity();
        if(machine.GetIsGrounded()){
            machine.ChangeState(PlayerStates.Idle);
        }
    }

    public override void OnExit()
    {
        anim.ResetTrigger(PlayerStates.Bouncing.ToString());
    }
}