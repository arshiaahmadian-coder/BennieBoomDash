using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private AudioClip explosionClip;
    [SerializeField] private AudioSource audioSource;

    private float minPitch = 0.9f;
    private float maxPitch = 1.1f;

    private void Start()
    {
        Invoke(nameof(DestroyIt), lifeTime);
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.PlayOneShot(explosionClip);
    }

    private void DestroyIt()
    {
        
        Destroy(gameObject);
    }
}
