using System;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _TriggerLifeTime = 0.3f;

    private void Update()
    {
        if (_TriggerLifeTime >= 0)
            _TriggerLifeTime -= Time.deltaTime;
        else
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.GetHurt(_attackDamage);
            Destroy(gameObject);
        }
    }

}
