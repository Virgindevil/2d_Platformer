using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerAnimation))]

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float _maxSecondsAfterAttack = 5f;
    [SerializeField] private float _minSecondsAfterAttack = 0.4f;
    [SerializeField] private float _SecondsToDropAttackRate = 1f;
    [Header("Префабы атаки:")]
    [SerializeField] private GameObject _lightAttackPrefab;
    [SerializeField] private Vector3 _lightAttackTriggerSpawnPosition;
    [SerializeField] private GameObject _heavyAttackPrefab;
    [SerializeField] private Vector3 _heavyAttackTriggerSpawnPosition;
    
    private SpriteRenderer _spriteRenderer;
    private PlayerAnimation _playerAnimation;
    private float _timeSinceAttack;
    private int _currentAttack = 1;

    private void Awake()
    {
        _playerAnimation = GetComponent<PlayerAnimation>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private GameObject SpawnAttack(GameObject prefab, Vector3 position)
    {
        var attackObject = Instantiate(prefab, transform);
        if (!_spriteRenderer.flipX)
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
    
    private void Update()
    {
        if (_timeSinceAttack <= _maxSecondsAfterAttack)
            _timeSinceAttack += Time.deltaTime;
        
        if(Input.GetMouseButtonDown(0) && _timeSinceAttack > _minSecondsAfterAttack)
        {
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

            if (_timeSinceAttack > _SecondsToDropAttackRate)
                _currentAttack = 1;

            _timeSinceAttack = 0.0f;
            
            _playerAnimation.SetAttackTrigger("Attack" + _currentAttack);
            
            _currentAttack++;
        }
    }
}