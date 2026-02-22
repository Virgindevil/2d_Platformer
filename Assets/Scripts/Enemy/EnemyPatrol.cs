using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float _patrolSpeed = 2f;
    [SerializeField] private float _wallCheckDistance = 0.6f;
    [SerializeField] private LayerMask _wallLayer;

    private Vector2 direction;
    private float xDirection;
    private bool _moveRight = true;
    private Rigidbody2D _rigidbody2D;

    private void Start() => _rigidbody2D = GetComponent<Rigidbody2D>();

    private void Update()
    {
        direction = _moveRight ? Vector2.right : Vector2.left;
        xDirection = _moveRight ? 1 : -1;
    }


    private void FixedUpdate()
    {
        if (Physics2D.Raycast(transform.position, direction, _wallCheckDistance, _wallLayer))
        {
            _moveRight = !_moveRight; 
        }
        _rigidbody2D.velocity = new Vector2(xDirection * _patrolSpeed, _rigidbody2D.velocity.y);
    }
}