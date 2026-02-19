using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float _patrolSpeed = 2f;
    [SerializeField] private float _wallCheckDistance = 0.6f;
    [SerializeField] private LayerMask _wallLayer;
    
    private bool _moveRight = true;
    private Rigidbody2D _rb;

    private void Start() => _rb = GetComponent<Rigidbody2D>();

    private void FixedUpdate()
    {
        // Проверяем стену впереди
        Vector2 direction = _moveRight ? Vector2.right : Vector2.left;
        
        if (Physics2D.Raycast(transform.position, direction, _wallCheckDistance, _wallLayer))
        {
            _moveRight = !_moveRight; // Меняем направление
        }

        // Движение
        float xDirection = _moveRight ? 1 : -1;
        _rb.velocity = new Vector2(xDirection * _patrolSpeed, _rb.velocity.y);
    }
}