using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(BoxCollider2D))]
public class PlayerControllerTest : MonoBehaviour
{
    [Header("Movement setting")]
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float jumpMoveSpeed = 7f;

    [Space(5)]
    [SerializeField] private float jumpValue = 0;
    [SerializeField] private float maxJump = 5;
    [SerializeField] private float jumpPerF = 0.1f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private ParticleSystem jumpDust;
    [SerializeField] private ParticleSystem stompP;
    [SerializeField] private float castDistance;
    [SerializeField] private float maxFall;
    [SerializeField] private PhysicsMaterial2D normalMat;
    [SerializeField] private PhysicsMaterial2D bounceMat;
    [SerializeField] private float moveDesaceleration = 1;
    [SerializeField] private float maxFallToStomp = 5;

    private enum State
    {
        Idle,
        Walking,
        Charging,
        Jumping,
        Falling,
        Bouncing,
        Stomping
    }

    [SerializeField] private State currentState;
    private float? fallMoment = null;
    private BoxCollider2D col;
    private Animator anim;
    private SpriteRenderer spriteR;
    private bool jumpPressed;
    private float lateralMove;
    private Rigidbody2D rb;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        spriteR = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        currentState = State.Idle;
    }

    void Update()
    {
        jumpPressed = Input.GetButton("Jump");
        lateralMove = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        CheckGround();

        switch (currentState)
        {
            case State.Idle:
                HandleIdle();
                break;
            case State.Walking:
                HandleRunning();
                break;
            case State.Charging:
                HandleCharge();
                break;
            case State.Jumping:
                HandleJumping();
                break;
            case State.Falling:
                HandleFalling();
                break;
            case State.Stomping:
                HandleStomping();
                break;
            case State.Bouncing:
                HandleBouncind();
                break;
        }
    }

    private void HandleBouncind()
    {
        ApplyFallVelocity();
        if(isGrounded){
            ChangeState(State.Idle);
        }
    }

    void HandleIdle()
    {
        Move();

        if (lateralMove != 0)
        {
            ChangeState(State.Walking);
        }
        else if (jumpPressed && isGrounded)
        {
            ChangeState(State.Charging);

        }else if (rb.velocity.y < 0)
        {
            ChangeState(State.Falling);
        }
    }

    void HandleRunning()
    {
        Move();

        if (lateralMove == 0)
        {
            ChangeState(State.Idle);
        }
        else if (jumpPressed && isGrounded)
        {
            ChangeState(State.Charging);

        }else if (rb.velocity.y < 0)
        {
            ChangeState(State.Falling);
        }
    }

    private void HandleCharge()
    {
        ChangeJump();

        if ((jumpValue >= maxJump || !jumpPressed && jumpValue >= 0.1f) && isGrounded)
        {
            ChangeState(State.Jumping);
        }
    }

    void HandleJumping()
    {
        Jump();

        if (rb.velocity.y < 0)
        {
            ChangeState(State.Falling);
        }
    }

    void HandleFalling()
    {
        ApplyFallVelocity();

        if(Stomp())
        {
            ChangeState(State.Stomping);

        }else if (isGrounded)
        {
            ChangeState(State.Idle);
        }
    }

    void HandleStomping()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
         if (lateralMove != 0)
        {
            ChangeState(State.Walking);
        }
    }

    private void ChangeState(State newState)
    {
        anim.ResetTrigger(currentState.ToString());
        currentState = newState;
        anim.SetTrigger(currentState.ToString());
    }

    private bool Stomp()
    {
        Vector2 velocity = rb.velocity;
        if (!isGrounded && velocity.y < 0 && fallMoment == null)
        {
            fallMoment = transform.position.y;
        }

        if (isGrounded && fallMoment != null)
        {
            float distance = Vector2.Distance(new Vector2(0, fallMoment.Value), new Vector2(0, transform.position.y));
            fallMoment = null;
            if (distance > maxFallToStomp)
            {
                
                return true;
            }
        }
        return false;
    }
    void ChangeJump(){
        if (jumpPressed && isGrounded)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
            jumpValue += jumpPerF;
            chageDirection();
        }
    }

    void Jump()
    {

        if ((jumpValue >= maxJump || !jumpPressed && jumpValue >= 0.1f) && isGrounded)
        {
            CreateDust();
            float tempx = lateralMove * jumpMoveSpeed;
            float tempy = jumpValue;
            rb.velocity = new Vector2(tempx, tempy);
            Invoke("ResetJump", 0.025f);
        }
    }


    void Move()
    {
        if (jumpValue == 0.0f && isGrounded)
        {
            rb.velocity = new Vector2(lateralMove * moveSpeed, rb.velocity.y);
            chageDirection();
        }
    }

    void ResetJump()
    {
        jumpValue = 0.0f;
    }

    void ApplyFallVelocity()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(
                Mathf.MoveTowards(rb.velocity.x, 0, moveDesaceleration * Time.fixedDeltaTime),
                Mathf.Max(rb.velocity.y, -maxFall)
            );
        }
    }

    void CheckGround()
    {
        isGrounded = Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundMask);
        col.sharedMaterial = isGrounded ? normalMat : bounceMat;
    }

    void CreateDust()
    {
        jumpDust.Play();
    }

    void chageDirection(){
        if (lateralMove != 0)
        {
            spriteR.flipX = lateralMove < 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

    void OnCollisionEnter2D(Collision2D collsion){

        if(!isGrounded && col.sharedMaterial == bounceMat){
            ChangeState(State.Bouncing);
        }

    }

}
