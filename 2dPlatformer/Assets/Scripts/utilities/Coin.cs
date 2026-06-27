using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CoinNumber.instance.AddCoin();
        }
        Destroy(gameObject);
    }
}
