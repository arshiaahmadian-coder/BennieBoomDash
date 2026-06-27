using UnityEngine;

public class Rocket45Deg : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform explosionPos;
    [SerializeField] private GameObject explosionObj;

    private void FixedUpdate()
    {
        float _speed = speed * Time.deltaTime;
        transform.Translate(_speed, 0, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerHealth.instance.TakeDamage(5);
        }

        Instantiate(explosionObj, explosionPos.position, explosionPos.rotation);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
