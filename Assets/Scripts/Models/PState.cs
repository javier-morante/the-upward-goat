using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class PState : State<PlayerController>
{
    protected Animator anim;

    protected Rigidbody2D rb;

    protected GatherInput inputs;

    protected PlayerStats playerStats;

    public PState(PlayerController machine, PlayerStats playerStats) : base(machine)
    {
        this.anim = machine.GetComponent<Animator>();
        this.rb = machine.GetComponent<Rigidbody2D>();
        this.inputs = machine.GetComponent<GatherInput>();
        this.playerStats = playerStats;
    }
}
