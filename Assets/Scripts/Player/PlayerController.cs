using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlayerControllerT : Subject<PlayerEvents>, IDataPersistence<GameData>
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5;

    [Space(10)]

    [Header("Jump")]
    [SerializeField] private float jumpMoveSpeed = 7f;
    [SerializeField] private float jumpValue = 0;
    [SerializeField] private float maxJump = 5;
    [SerializeField] private float jumpPerF = 0.1f;

    [Space(10)]

    [Header("Ground and Slide check")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask slideMask;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private Vector2 boxSize2;
    [SerializeField] private float castDistance;

    [Space(10)]

    [Header("Fall")]
    [SerializeField] private float maxFall;
    [SerializeField] private float moveDesacelerationFalling = 1;
    [SerializeField] private float moveDesacelerationBounce = 1;
    [SerializeField] private float maxFallToStomp = 5;

    [Space(10)]

    [Header("PhysicMaterails")]
    [SerializeField] private PhysicsMaterial2D normalMat;
    [SerializeField] private PhysicsMaterial2D bounceMat;

    [Header("Particles")]

    [SerializeField] private ParticleSystem stompP;
    [SerializeField] private ParticleSystem jumpDust;

    [Space(10)]

    [Header("Sounds")]
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip fall;
    [SerializeField] private AudioClip stomp;
    [SerializeField] private AudioClip bounce;
    [SerializeField] private AudioClip coin;



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
    private bool isSlide;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        spriteR = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        UiManager UiManager = GameObject.Find("GameManager").GetComponent<UiManager>();
        this.AddObserver(UiManager);
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


    void HandleIdle()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);

        if (lateralMove != 0 && (!isSlide || rb.velocity.y == 0))
        {
            ChangeState(State.Walking);
        }
        else if (jumpPressed && isGrounded)
        {
            ChangeState(State.Charging);
        }
        else if (rb.velocity.y < 0 && !isGrounded)
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

        }
        else if (rb.velocity.y < 0 && !isGrounded)
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
            AudioManager.instance.PlaySoundFX(jump, transform, 1f);
            ChangeState(State.Jumping);
        }
        else if (rb.velocity.y < 0)
        {
            ChangeState(State.Falling);
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

        if (Stomp())
        {
            StompParticles();
            ChangeState(State.Stomping);
            AudioManager.instance.PlaySoundFX(stomp, transform, 1f);

        }
        else if (isGrounded && rb.velocity.y == 0f)
        {
            ChangeState(State.Idle);
            AudioManager.instance.PlaySoundFX(fall, transform, 1f);
        }
    }
    private void HandleBouncind()
    {
        ApplyFallVelocity(moveDesacelerationBounce);
        if (Stomp())
        {
            StompParticles();
            ChangeState(State.Stomping);
            AudioManager.instance.PlaySoundFX(stomp, transform, 1f);

        }
        else if (isGrounded && rb.velocity.y == 0f)
        {
            ChangeState(State.Idle);
            AudioManager.instance.PlaySoundFX(fall, transform, 1f);
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
    void ChargeJump()
    {
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
        isSlide = Physics2D.BoxCast(transform.position, boxSize2, 0, -transform.up, castDistance, slideMask);
        col.sharedMaterial = (isGrounded || isSlide) ? normalMat : bounceMat;
    }

    void CreateDust()
    {
        jumpDust.Play();
    }

    void chageDirection()
    {
        if (lateralMove != 0)
        {
            spriteR.flipX = lateralMove < 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize2);
    }

    void OnCollisionEnter2D(Collision2D collsion)
    {

        if (!isGrounded && col.sharedMaterial == bounceMat)
        {
            AudioManager.instance.PlaySoundFX(bounce, transform, 1f);
            ChangeState(State.Bouncing);
        }
    }

    void StompParticles(){
        Vector2 tParticle = transform.position;
        tParticle.y -= col.size.y;
        ParticleSystem particle = Instantiate(stompP,tParticle,stompP.transform.rotation);
        particle.Play();
    }

    public void LoadData(GameData gameData)
    {
        this.transform.position = gameData.playerPosition;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.playerPosition = this.transform.position;
    }
}