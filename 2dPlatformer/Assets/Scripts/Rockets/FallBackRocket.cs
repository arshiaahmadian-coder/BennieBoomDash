using UnityEngine;

public class FallBackRocket : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float activationDistance;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioSource audioSource;
    private PlayerHealth player;
    private bool hasActivated;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();
    }
    
    private void FixedUpdate()
    {
        if (!hasActivated) return;

        float _speed = speed * Time.deltaTime;
        transform.Translate(_speed, 0, 0);

        if (transform.position.y > 20)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(!hasActivated)
        {
            float distance = transform.position.x - player.transform.position.x;
            if (distance <= activationDistance)
            {
                hasActivated = true;
                audioSource.PlayOneShot(jumpClip);
            }
        }
    }
}
