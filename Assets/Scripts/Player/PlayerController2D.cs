using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerGroundCheck))]

public class PlayerController2D : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce = 8;    
   
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private PlayerGroundCheck _groundCheck;
    private int _facingDirection = 0;
    private const string InputXName = "Horizontal";
    private const string InputJumpName = "space";
    
    public float InputX { get; private set; }
    
    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _groundCheck = GetComponent<PlayerGroundCheck>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update ()
    {
        if (!IsGrounded && _groundCheck.State())
        {
            IsGrounded = true;
        }

        if (IsGrounded && !_groundCheck.State())
        {
            IsGrounded = false;
        }
        
        
        InputX = Input.GetAxis(InputXName);

        if (InputX > 0)
        {
            _spriteRenderer.flipX = false;
            _facingDirection = 1;
        }
            
        else if (InputX < 0)
        {
            _spriteRenderer.flipX = true;
            _facingDirection = -1;
        }
        
        if (Input.GetKeyDown(InputJumpName) && IsGrounded)
        {
            IsGrounded = false;
            _rigidbody2D.velocity = new Vector2(RigidbodyVelocityY, _jumpForce);
            _groundCheck.Disable(0.2f);
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(InputX * _moveSpeed, _rigidbody2D.velocity.y);
    }
    
    public float RigidbodyVelocityX
    {
        get
        {
            return _rigidbody2D.velocity.x;
        }
    }
    
    public float RigidbodyVelocityY
    {
        get
        {
            return _rigidbody2D.velocity.y;
        }
    }

    public void SetVelocity(Vector2 velocity)
    {
        _rigidbody2D.velocity = velocity;
    }
}
