using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _HealthPoints = 100f;
    
    private SpriteRenderer _spriteRenderer;
    private Color _baseColor;
    
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _baseColor = _spriteRenderer.color;
    }
    
    public void GetHurt(float damage)
    {
        _HealthPoints -= damage;
        _spriteRenderer.color = Color.red;
        Debug.Log(_HealthPoints);
    }

    private void FixedUpdate()
    {
        if (_spriteRenderer.color != _baseColor)
        {
            _spriteRenderer.color = _baseColor;
        }
        
        if (_HealthPoints <= 0)   
            Destroy(gameObject);
    }
}
