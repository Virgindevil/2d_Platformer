using UnityEngine;

public class CoinIdle : MonoBehaviour
{
    [SerializeField] private float _amplitude = 0.3f;
    [SerializeField] private float _speed = 2f;
    
    private Vector3 _startPos;

    private void Start()
    {
        _startPos = transform.localPosition;
    }

    private void Update()
    {
        float newY = _startPos.y + Mathf.Sin(Time.time * _speed) * _amplitude;
        transform.localPosition = new Vector3(_startPos.x, newY, _startPos.z);
    }
}
