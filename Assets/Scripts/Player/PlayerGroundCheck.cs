using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerGroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask _gronudLayerMask;
    [SerializeField] private float _castDistanse = 0.1f;

    private Rigidbody2D _rigidbody2D;
    private CapsuleCollider2D capsuleCollider2D;
    private ContactFilter2D _contactFilter2D;
    private RaycastHit2D[] _hitResults = new RaycastHit2D[1];

    public float _disableTimer { get; private set; }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();

        _contactFilter2D.useLayerMask = true;
        _contactFilter2D.SetLayerMask(_gronudLayerMask);
        _contactFilter2D.useTriggers = false;
    }

    public bool State()
    {
        if (_disableTimer > 0)
            return false;

        int hitCount = _rigidbody2D.Cast(Vector2.down, _contactFilter2D, _hitResults, _castDistanse);
        return hitCount > 0;
    }

    void Update()
    {
        if (_disableTimer > 0)    
            _disableTimer -= Time.deltaTime;
    }

    public void Disable(float duration)
    {
        _disableTimer = duration;
    }

    private void OnDrawGizmos()
    {
        if (capsuleCollider2D == null) capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        if (capsuleCollider2D == null) return;

        bool isGrounded = Application.isPlaying ? State() : false;
        Gizmos.color = isGrounded ? Color.green : Color.red;

        Vector3 castPosition = transform.position + (Vector3)(Vector2.down * _castDistanse);
        
        Gizmos.DrawWireSphere(castPosition + (Vector3)capsuleCollider2D.offset + Vector3.up * (capsuleCollider2D.size.y / 2 - capsuleCollider2D.size.x / 2), capsuleCollider2D.size.x / 2);
        Gizmos.DrawWireSphere(castPosition + (Vector3)capsuleCollider2D.offset - Vector3.up * (capsuleCollider2D.size.y / 2 - capsuleCollider2D.size.x / 2), capsuleCollider2D.size.x / 2);
    }
}