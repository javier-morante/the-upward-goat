using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Stats")]
    public PlayerStats playerStats;

    [Header("Particles")]

    public ParticleSystem stompP;
    public ParticleSystem jumpDust;
    /*-----------------------------------------------*/

    private BoxCollider2D col;

    private Rigidbody2D rb;

    private bool isGrounded;

    private GatherInput inputs;

    private SpriteRenderer spriteR;
    private float move;
    private bool jump;


    /* --------------------------------- */

    private State<PlayerController> currentState;

    private Dictionary<PlayerStates,State<PlayerController>> dicState = new Dictionary<PlayerStates, State<PlayerController>>();
     [SerializeField] private Vector2 boxSize;
    [SerializeField] private float castDistance;
    [SerializeField] private LayerMask groundMask;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        inputs = GetComponent<GatherInput>();
        spriteR = GetComponent<SpriteRenderer>();

        dicState.Add(PlayerStates.Idle,new IdleState(this,playerStats));
        dicState.Add(PlayerStates.Walking,new WalkState(this, playerStats));
        dicState.Add(PlayerStates.Charging,new ChargeState(this,playerStats));
        dicState.Add(PlayerStates.Jumping,new JumpState(this,playerStats));
        dicState.Add(PlayerStates.Falling,new FallState(this,playerStats));
        dicState.Add(PlayerStates.Bouncing,new BounceState(this,playerStats));
        dicState.Add(PlayerStates.Stomping,new StompState(this,playerStats));
        
        currentState = dicState[PlayerStates.Idle];
        
    }

    void Start()
    {
        currentState.OnEnter();
    }
  
    void Update(){
        currentState?.OnUpdate();
    }

    void FixedUpdate(){
        CheckGround();

        currentState?.OnPhysicsUpdate();
    }

    void CheckGround()
    {
        isGrounded = Physics2D.BoxCast(transform.position, playerStats.boxSize, 0, -transform.up, playerStats.castDistance, playerStats.groundMask);
        col.sharedMaterial = isGrounded ? playerStats.normalMat : playerStats.bounceMat;
    }

    internal void ChangeState(PlayerStates state)
    {
        Debug.Log("ChangeState"+state);
        currentState.OnExit();
        currentState = dicState[state];
        currentState.OnEnter();
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    public void ApplyFallVelocity()
    {
        rb.velocity = new Vector2(
            Mathf.MoveTowards(rb.velocity.x, 0, playerStats.moveDesaceleration * Time.fixedDeltaTime),
            Mathf.Max(rb.velocity.y, -playerStats.maxFall)
        );
    }    

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

    public float GetMove(){
        return move;
    }

    public bool GetJump(){
        return jump;
    }
    
    public void ChangeDirection(){
        if (inputs.inputData.move != 0)
        {
            spriteR.flipX = inputs.inputData.move < 0;
        }
    }
    void OnCollisionEnter2D(Collision2D collsion){

        if(!isGrounded && col.sharedMaterial == playerStats.bounceMat){
            ChangeState(PlayerStates.Bouncing);
        }

    }

}