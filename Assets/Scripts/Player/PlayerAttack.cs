using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _triggerLifeTime = 0.3f;

    private void Update()
    {
        if (_triggerLifeTime >= 0)
            _triggerLifeTime -= Time.deltaTime;
        else
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Enemy>())
        {
            var enemy = other.gameObject.GetComponent<Enemy>();
            enemy.GetHurt(_attackDamage);
            Destroy(gameObject);
        }
    }
}