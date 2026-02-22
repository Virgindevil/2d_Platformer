using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _healthPoints = 100f;
    [SerializeField] private float _timeToReturnBaseColor = 1f;
    
    private SpriteRenderer _spriteRenderer;
    private Color _baseColor;
    private float _timeToBaseColor;
    
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _baseColor = _spriteRenderer.color;
    }
    
    public void GetHurt(float damage)
    {
        _healthPoints -= damage;
        _spriteRenderer.color = Color.red;
        _timeToBaseColor = _timeToReturnBaseColor;
    }

    private void Update()
    {
        if (_timeToBaseColor >= 0)
            _timeToBaseColor -= Time.deltaTime;
        
        if (_spriteRenderer.color != _baseColor && _timeToBaseColor <= 0)
        {
            _spriteRenderer.color = _baseColor;
        }
        
        if (_healthPoints <= 0)   
            Destroy(gameObject);
    }
}
