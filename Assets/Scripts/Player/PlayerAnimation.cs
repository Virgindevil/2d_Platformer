using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerController2D))]
[RequireComponent(typeof(PlayerAttack))]

public class PlayerAnimation : MonoBehaviour
{
    private PlayerController2D _playerController;
    private PlayerAttack _playerAttack;
    private Animator _animator;
    private float _delayToIdle = 0.0f;
    
    private void Awake()
    {
        _playerController =  GetComponent<PlayerController2D>();
        _playerAttack =  GetComponent<PlayerAttack>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool("Grounded", _playerController.IsGrounded);
        _animator.SetFloat("AirSpeedY", _playerController.RigidbodyVelocityY);
        

        if (Input.GetKeyDown("space") && _playerController.IsGrounded)
        {
            _animator.SetTrigger("Jump");
        }

        if (Mathf.Abs(_playerController.InputX) > Mathf.Epsilon)
        {
            _delayToIdle = 0.05f;
            _animator.SetInteger("AnimState", 1);
        }
        else
        {
            _delayToIdle -= Time.deltaTime;
            if(_delayToIdle < 0)
                _animator.SetInteger("AnimState", 0);
        }
    }

    public void SetAttackTrigger(string triggerName)
    {
        _animator.SetTrigger(triggerName);
    }
}
