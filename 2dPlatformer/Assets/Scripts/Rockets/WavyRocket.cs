using UnityEngine;

public class WavyRocket : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float amplitude;
    [SerializeField] private float frequency;
    [SerializeField] private float removeDistance;
    [SerializeField] private Transform explosionPos;
    [SerializeField] private GameObject explosionObj;

    private Transform playerTransform;
    private float waveTime;
    private int direction = -1;

    void Start()
    {
        playerTransform = FindFirstObjectByType<PlayerMovement>().transform;
        waveTime = Random.Range(0f, 10f);
    }

    void FixedUpdate()
    {
        float dt = Time.deltaTime;

        transform.Translate(direction * speed * dt, 0, 0);

        waveTime += dt * frequency;
        float yOffset = Mathf.Sin(waveTime) * amplitude * dt;
        transform.Translate(0, yOffset, 0);

        if (transform.position.x < playerTransform.position.x - removeDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            PlayerHealth.instance.TakeDamage(5);

        Instantiate(explosionObj, explosionPos.position, explosionPos.rotation);
        Destroy(gameObject);
    }
}
