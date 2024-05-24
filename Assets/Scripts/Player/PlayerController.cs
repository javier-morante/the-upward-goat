using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(BoxCollider2D))]
public class PlayerControllerT : Subject<PlayerEvents>
{
    [Header("Movement")]
     public float moveSpeed = 5;

    [Space(10)]

    [Header("Jump")]
     public float jumpMoveSpeed = 7f;
     public float jumpValue = 0;
     public float maxJump = 5;
     public float jumpPerF = 0.1f;

    [Space(10)]

    [Header("Ground Check")]
     public LayerMask groundMask;
     public Vector2 boxSize;
     public float castDistance;

    [Space(10)]

    [Header("Fall")]
     public float maxFall;
     public float moveDesacelerationFalling = 1;
     public float moveDesacelerationBounce = 1;
     public float maxFallToStomp = 5;

    [Space(10)]

    [Header("PhysicMaterails")]
     public PhysicsMaterial2D normalMat;
     public PhysicsMaterial2D bounceMat;

    [Header("Particles")]

     public ParticleSystem stompP;
     public ParticleSystem jumpDust;

    [Space(10)]

    [Header("Sounds")]
    public AudioClip jump;
    public AudioClip fall;
    public AudioClip stomp;
    public AudioClip bounce;
    public AudioClip coin;



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

    private State currentState;
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
        ApplyFallVelocity(moveDesacelerationBounce);
        if(Stomp())
        {
            ChangeState(State.Stomping);
            AudioManager.instance.PlaySoundFX(stomp,transform,1f);

        }else if (isGrounded && rb.velocity.y == 0f)
        {
            Debug.Log("caido");
            ChangeState(State.Idle);
            AudioManager.instance.PlaySoundFX(fall,transform,1f);
        }
    }

    void HandleIdle()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);

        if (lateralMove != 0)
        {
            ChangeState(State.Walking);
        }
        else if (jumpPressed && isGrounded)
        {
            ChangeState(State.Charging);

        }else if (rb.velocity.y < 0 && !isGrounded)
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

        }else if (rb.velocity.y < 0 && !isGrounded)
        {
            ChangeState(State.Falling);
        }
    }

    private void HandleCharge()
    {
        ChargeJump();

        if ((jumpValue >= maxJump || !jumpPressed && jumpValue >= 0.1f) && isGrounded)
        {
            this.NotifyObservers(PlayerEvents.Jump);
            AudioManager.instance.PlaySoundFX(jump,transform,1f);
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
        ApplyFallVelocity(moveDesacelerationFalling);

        if(Stomp())
        {
            ChangeState(State.Stomping);
            AudioManager.instance.PlaySoundFX(stomp,transform,1f);

        }else if (isGrounded && rb.velocity.y == 0f)
        {
            Debug.Log("caido");
            ChangeState(State.Idle);
            AudioManager.instance.PlaySoundFX(fall,transform,1f);
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
    void ChargeJump(){
        
        rb.velocity = new Vector2(0f, rb.velocity.y);
        jumpValue += jumpPerF;
        chageDirection();
        
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
        
        rb.velocity = new Vector2(lateralMove * moveSpeed, rb.velocity.y);
        chageDirection();
        
    }

    void ResetJump()
    {
        jumpValue = 0.0f;
    }

    void ApplyFallVelocity(float deseleration)
    {
        
        rb.velocity = new Vector2(
            Mathf.MoveTowards(rb.velocity.x, 0, deseleration * Time.fixedDeltaTime),
            Mathf.Max(rb.velocity.y, -maxFall)
        );
        
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
            AudioManager.instance.PlaySoundFX(bounce,transform,1f);
            ChangeState(State.Bouncing);
        }
    }

    void OnTriggerEnter2D(Collider2D collsion){
        if(collsion.gameObject.tag == "Coin"){
            this.NotifyObservers(PlayerEvents.CoinCollected);
            AudioManager.instance.PlaySoundFX(coin,transform,1f);
            Destroy(collsion.gameObject);
        }
    }
    
}