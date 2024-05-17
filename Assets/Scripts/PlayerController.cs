using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.Tilemaps;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float jumpValue = 0;
    [SerializeField] private float maxJump = 5;
    [SerializeField] private float jumpPerF = 0.1f;
    [SerializeField] private LayerMask goundMarsk;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float castDistance;
    [SerializeField] private float maxFall;
    [SerializeField] private PhysicsMaterial2D normalMat;
    [SerializeField] private PhysicsMaterial2D bounceMat;
    [SerializeField] private float moveDesaceleration = 1;
    [SerializeField] private float jumpMoveSpeed = 7f;
    [SerializeField] private ParticleSystem jumpDust;
    

    private BoxCollider2D _col;
    private bool _jumpPreesed;
    private float _lateralMove;
    private float screenWidth;
    private float screenHeight;
    private Rigidbody2D _rb;
    private bool _isGrounded;

    void Awake(){
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<BoxCollider2D>();
    }
    // void Start()
    // {
    //     _rb = GetComponent<Rigidbody2D>();
    //     _col = GetComponent<BoxCollider2D>();
    // }

    // Update is called once per frame
    void Update()
    {
        _jumpPreesed = Input.GetButton("Jump");
        _lateralMove = Input.GetAxisRaw("Horizontal");

        screenWidth = (Camera.main.orthographicSize * Screen.width / Screen.height) * 2;
        screenHeight = Camera.main.orthographicSize * 2;
        if (transform.position.x < Camera.main.transform.position.x - screenWidth / 2)
        {
            Debug.Log("out of camara");
        }
    }

    void FixedUpdate()
    {
        isGrounded();
        if (_rb.velocity.y < 0){
            _rb.velocity = new Vector2(Mathf.MoveTowards(_rb.velocity.x,0,moveDesaceleration*Time.fixedDeltaTime),Mathf.Max(_rb.velocity.y,-maxFall));
        }
        Jump();
        Move();
    }

    void Jump()
    {
        
        if (_jumpPreesed && _isGrounded)
        {
            _rb.velocity = new Vector2(0f, _rb.velocity.y);
            jumpValue += jumpPerF;
            
        }


        if ((jumpValue >= maxJump || !_jumpPreesed && jumpValue >= 0.1f) && _isGrounded)
        {
            CreateDust();
            float tempx = _lateralMove * jumpMoveSpeed;
            float tempy = jumpValue;
            _rb.velocity = new Vector2(tempx, tempy);
            Invoke("ResetJump", 0.025f);
        }
    }

    void Move()
    {
        if (jumpValue == 0.0f && _isGrounded)
        {
            _rb.velocity = new Vector2(_lateralMove*moveSpeed,_rb.velocity.y);
        }
    }

    void ResetJump()
    {
        jumpValue = 0.0f;
    }

    void isGrounded()
    {
        _isGrounded = Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, goundMarsk);

        _col.sharedMaterial = _isGrounded? normalMat : bounceMat;
        
    }

    void CreateDust()
    {
        jumpDust.Play();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position-transform.up * castDistance, boxSize);
    }
}
