using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerGroundCheck))]

public class PlayerController2D : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField]public float _jumpForce = 10;
    
   
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    public PlayerGroundCheck _groundCheck { get; private set; }
    
    public float inputX { get; private set; }
    private int _facingDirection = 0;
    public bool _isGrounded { get; private set; }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _groundCheck = GetComponent<PlayerGroundCheck>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update ()
    {
        if (!_isGrounded && _groundCheck.State())
        {
            _isGrounded = true;
        }

        if (_isGrounded && !_groundCheck.State())
        {
            _isGrounded = false;
        }
        
        
        inputX = Input.GetAxis("Horizontal");

        if (inputX > 0)
        {
            _spriteRenderer.flipX = false;
            _facingDirection = 1;
        }
            
        else if (inputX < 0)
        {
            _spriteRenderer.flipX = true;
            _facingDirection = -1;
        }
        
        if (Input.GetKeyDown("space") && _isGrounded)
        {
            _isGrounded = false;
            _rigidbody2D.velocity = new Vector2(RigidbodyVelocityY, _jumpForce);
            _groundCheck.Disable(0.2f);
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(inputX * _moveSpeed, _rigidbody2D.velocity.y);
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
