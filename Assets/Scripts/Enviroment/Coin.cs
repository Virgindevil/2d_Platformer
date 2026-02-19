using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Coin : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController2D>())
            CollectCoin();
    }

    private void CollectCoin()
    {
        Debug.Log("Collected Coin");
        Destroy(gameObject);
    }
}
