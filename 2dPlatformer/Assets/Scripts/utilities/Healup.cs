using UnityEngine;

public class Healup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerHealth.instance.GetHealth(30);
        }
        Destroy(gameObject);
    }
}
