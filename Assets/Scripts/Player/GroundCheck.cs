using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public int m_ColCount { get; private set; }

    public float m_DisableTimer { get; private set; }

    private void OnEnable()
    {
        m_ColCount = 0;
    }

    public bool State()
    {
        if (m_DisableTimer > 0)
            return false;
        return m_ColCount > 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        m_ColCount++;
    }
    

    void OnTriggerExit2D(Collider2D other)
    {
        m_ColCount--;
    }

    void Update()
    {
        if (m_DisableTimer >= 0)    
            m_DisableTimer -= Time.deltaTime;
    }

    public void Disable(float duration)
    {
        m_DisableTimer = duration;
    }
}
