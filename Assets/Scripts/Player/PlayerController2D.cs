using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerController2D : MonoBehaviour
{
    [Header("Скорость ходьбы:")]
    [SerializeField] private float _moveSpeed;
    [Header("Сила прыжка:")]
    [SerializeField] private float _jumpForce;
    [Header("Префабы атаки:")]
    [SerializeField] private GameObject _lightAttackPrefab;
    [SerializeField] private Vector3 _lightAttackTriggerSpawnPosition;
    [SerializeField] private GameObject _heavyAttackPrefab;
    [SerializeField] private Vector3 _heavyAttackTriggerSpawnPosition;
   
    private Rigidbody2D _rb;
    private Animator _anim;
    private GroundCheck _groundCheck;
    
    private float _timeSinceAttack = 0.0f;
    private float _delayToIdle = 0.0f;
    private int _facingDirection = 0;
    private int _currentAttack = 0;
    private bool _isGrounded;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _groundCheck = GetComponentInChildren<GroundCheck>();
    }
    
    private GameObject SpawnAttack(GameObject prefab, Vector3 position)
    {
        var attackObject = Instantiate(prefab, this.transform);
        if (!GetComponent<SpriteRenderer>().flipX)
        {
            attackObject.transform.localPosition = position;
            return attackObject;  
        }
        else
        {
            attackObject.transform.localPosition = new Vector3(-position.x, position.y, position.z);
            return attackObject;  
        }
          
    }
    
    void Update ()
    {
        if (_timeSinceAttack <= 5)
            _timeSinceAttack += Time.deltaTime;

        if (!_isGrounded && _groundCheck.State())
        {
            _isGrounded = true;
            _anim.SetBool("Grounded", _isGrounded);
        }

        if (_isGrounded && !_groundCheck.State())
        {
            _isGrounded = false;
            _anim.SetBool("Grounded", _isGrounded);
        }

        float inputX = Input.GetAxis("Horizontal");

        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            _facingDirection = 1;
        }
            
        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            _facingDirection = -1;
        }

        _rb.velocity = new Vector2(inputX * _moveSpeed, _rb.velocity.y);

        _anim.SetFloat("AirSpeedY", _rb.velocity.y);

        
        if(Input.GetMouseButtonDown(0) && _timeSinceAttack > 0.25f)
        {
            _currentAttack++;

            switch (_currentAttack)
            {
                case 1:
                    SpawnAttack(_lightAttackPrefab,_lightAttackTriggerSpawnPosition );
                    break;
                case 2:
                    SpawnAttack(_lightAttackPrefab, _lightAttackTriggerSpawnPosition);
                    break;
                case 3:
                    SpawnAttack(_heavyAttackPrefab, _heavyAttackTriggerSpawnPosition);
                    break;
            }

            if (_currentAttack > 3)
                _currentAttack = 1;

            if (_timeSinceAttack > 1.0f)
                _currentAttack = 1;

            _anim.SetTrigger("Attack" + _currentAttack);

            _timeSinceAttack = 0.0f;
        }
        
        else if (Input.GetKeyDown("space") && _isGrounded)
        {
            _anim.SetTrigger("Jump");
            _isGrounded = false;
            _anim.SetBool("Grounded", _isGrounded);
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            _groundCheck.Disable(0.2f);
        }

        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            // Reset timer
            _delayToIdle = 0.05f;
            _anim.SetInteger("AnimState", 1);
        }

        else
        {
            // Prevents flickering transitions to idle
            _delayToIdle -= Time.deltaTime;
                if(_delayToIdle < 0)
                    _anim.SetInteger("AnimState", 0);
        }
    }
}
