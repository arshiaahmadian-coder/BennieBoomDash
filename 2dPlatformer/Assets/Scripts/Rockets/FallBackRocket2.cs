using Unity.VisualScripting;
using UnityEngine;

public class FallBackRocket2 : MonoBehaviour
{
    private Transform target;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float speedDecreaseStep;
    [SerializeField] private float rotateBoostDelay;
    [SerializeField] private float maxRotateSpeed;
    [SerializeField] private float rotateBoostStep;

    [SerializeField] private float rotateSpeed;
    [SerializeField] private float activationDistance;
    [SerializeField] Transform explosionPos;
    [SerializeField] private GameObject explosionObj;
    
    Rigidbody2D rb;
    private bool boostRotate = false;
    private bool canRotate = true;
    private bool hasActivated = false;
    private bool EnableBoostRotateFlag = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        int digit = Random.Range(0, 2);
        if (digit == 0) target = PlayerMovement.instance.Head;
        else if (digit == 1) target = PlayerMovement.instance.Foots;
    }

    void FixedUpdate()
    {
        if (!hasActivated) return;

        if (EnableBoostRotateFlag)
        {
            Invoke(nameof(EnableBoostRotate), rotateBoostDelay);
            EnableBoostRotateFlag = false;
        }

        Vector2 dir = (target.position - transform.position).normalized;
        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotateSpeed * Time.fixedDeltaTime);
        if (canRotate) transform.rotation = Quaternion.Euler(0, 0, angle);
        rb.linearVelocity = transform.right * jumpSpeed;

        if (jumpSpeed > speed)
        {
            jumpSpeed -= speedDecreaseStep;
        }

        if (boostRotate)
        {
            if (rotateSpeed < maxRotateSpeed) rotateSpeed += rotateBoostStep;
            if (transform.rotation.z == 180) canRotate = false;
            if (target.position.x < transform.position.x) canRotate = false; 

            float distance = target.position.x - transform.position.x;
            if (distance <= (activationDistance * -1) && target.position.x < transform.position.x)
            {
                Instantiate(explosionObj, explosionPos.position, explosionPos.rotation);
                destroyIt();
            }
        }
    }

    private void Update()
    {
        if(!hasActivated)
        {
            float distance = transform.position.x - target.position.x;
            if (distance <= activationDistance)
            {
                hasActivated = true;
            }
        }
    }

    private void EnableBoostRotate()
    {
        boostRotate = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(explosionObj, explosionPos.position, explosionPos.rotation);
        destroyIt();

        if (other.tag == "Player")
        {
            PlayerHealth.instance.TakeDamage(5);
        }
    }

    private void destroyIt()
    {
        Destroy(gameObject);
    }
}
