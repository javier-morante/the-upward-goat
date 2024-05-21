using UnityEngine;

class FallState : PState
{
    private float? fallMoment;
    private Transform player;

    public FallState(PlayerController machine, PlayerStats playerStats) : base(machine,playerStats)
    {
        player = machine.transform;
    }

    public override void OnEnter()
    {
        anim.SetTrigger(PlayerStates.Falling.ToString());
    }

    public override void OnPhysicsUpdate()
    {
        machine.ApplyFallVelocity();

        if(Stomp())
        {
            machine.ChangeState(PlayerStates.Stomping);

        }else if (machine.GetIsGrounded())
        {
            machine.ChangeState(PlayerStates.Idle);
        }
    }

    public override void OnExit()
    {
        anim.ResetTrigger(PlayerStates.Falling.ToString());
    }

    private bool Stomp()
    {
        Vector2 velocity = rb.velocity;
        if (!machine.GetIsGrounded() && velocity.y < 0 && fallMoment == null)
        {
            fallMoment = player.position.y;
        }

        if (machine.GetIsGrounded() && fallMoment != null)
        {
            float distance = Vector2.Distance(new Vector2(0, fallMoment.Value), new Vector2(0, player.position.y));
            fallMoment = null;
            if (distance > playerStats.maxFallToStomp)
            {
                return true;
            }
        }
        return false;
    }
}