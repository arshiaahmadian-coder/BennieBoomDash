using UnityEngine;

public class StraightRocket : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float HeadPos;
    [SerializeField] private float LegPos;
    [SerializeField] private float activationDistance;
    [SerializeField] private float removeDistance;
    [SerializeField] private Transform explosionPos;
    [SerializeField] private GameObject explosionObj;

    private Transform playerTransform;

    private void Start()
    {
        playerTransform = FindFirstObjectByType<PlayerMovement>().transform;

        int digit = Random.Range(0, 2);
        if (digit == 0) transform.position = new Vector3(transform.position.x, 
         PlayerMovement.instance.Head.position.y, transform.position.z);
        else if (digit == 1) transform.position = new Vector3(transform.position.x, 
         PlayerMovement.instance.Foots.position.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        float _speed = speed * Time.deltaTime;
        transform.Translate(_speed, 0, 0);

        float distance = transform.position.x - playerTransform.position.x;
        if (distance  <= removeDistance)
        {
            Destroy(gameObject);
        }
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
}
