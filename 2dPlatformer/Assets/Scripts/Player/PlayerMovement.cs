using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float sitWalkSpeed;
    [SerializeField] private float jumpMoveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCD;

    [Header("Joystick Thresholds")]
    [SerializeField] private float walkThreshold = 0.3f;
    [SerializeField] private float jumpSitThreshold = 0.5f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckDistance = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("sit collider")]
    [SerializeField] private float sitYOfset;
    [SerializeField] private float sitYSize;

    private Rigidbody2D rb;
    private Joystick joystick;
    public Animator animator;
    private bool canJump = true;
    private bool isSitting = false;
    private bool isGrounded;
    private bool canMove = true;
    private CapsuleCollider2D playerCollider;

    public Transform Head;
    public Transform Foots;

    private float standYOfset;
    private float standYSize;

    public static PlayerMovement instance;
    void Awake() { instance = this; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        joystick = FindFirstObjectByType<Joystick>();
        animator = GetComponent<Animator>();

        playerCollider = GetComponent<CapsuleCollider2D>();
        standYOfset = playerCollider.offset.y;
        standYSize = playerCollider.size.y;
    }

    private void Update()
    {
        if (!isGrounded)
            if (rb.linearVelocity.y > 0.1f)
                animator.SetBool("isRising", true);
            else if (rb.linearVelocity.y < -0.1f)
                animator.SetBool("isRising", false);

        if (isSitting)
        {
            playerCollider.offset = new Vector2(playerCollider.offset.x, sitYOfset);
            playerCollider.size = new Vector2(playerCollider.size.x, sitYSize);
        } else if (standYSize != playerCollider.size.y)
        {
            playerCollider.offset = new Vector2(playerCollider.offset.x, standYOfset);
            playerCollider.size = new Vector2(playerCollider.size.x, standYSize);
        }
    }

    void FixedUpdate()
    {
        GroundCheck();

        float h = joystick.Horizontal;
        float v = joystick.Vertical;

        float absH = Mathf.Abs(h);
        float speed = absH < 0.7f ? walkSpeed : runSpeed;

        if (v < -jumpSitThreshold)
        {
            isSitting = true;
        }
        else
        {
            isSitting = false;
        }
        animator.SetBool("isSitting", isSitting);

        if (absH < walkThreshold)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
        }
        else
        {
            if(canMove)
            {
                float currentSpeed = isSitting ? sitWalkSpeed : speed;
                currentSpeed = isGrounded ? currentSpeed : jumpMoveSpeed;
                rb.linearVelocity = new Vector2(Mathf.Sign(h) * currentSpeed, rb.linearVelocity.y);

                animator.SetBool("isWalking", speed == walkSpeed);
                animator.SetBool("isRunning", speed == runSpeed);
            }

            Flip(h);
        }

        // jump
        if (v > jumpSitThreshold && isGrounded && canJump)
        {
            animator.SetBool("isRising", true);

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            animator.SetTrigger("jump");
            canJump = false;
            Invoke(nameof(ResetJumpCD), jumpCD);
        }
    }

    void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            groundCheckPoint.position,
            Vector2.down,
            groundCheckDistance,
            groundLayer
        );

        bool wasGrounded = isGrounded;
        isGrounded = hit.collider != null;
        animator.SetBool("isGrounded", isGrounded);

        if (!wasGrounded && isGrounded)
        {
            animator.SetBool("isRising", false);
        }
    }

    void Flip(float h)
    {
        if (h > 0.05f)
            transform.localScale = new Vector3(2.5f, transform.localScale.y, transform.localScale.z);
        else if (h < -0.05f)
            transform.localScale = new Vector3(-2.5f, transform.localScale.y, transform.localScale.z);
    }

    private void OnDrawGizmos()
    {
        if (groundCheckPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            groundCheckPoint.position,
            groundCheckPoint.position + Vector3.down * groundCheckDistance
        );
    }

    private void ResetJumpCD()
    {
        canJump = true;
    }

    public void ReverseCanMove()
    {
        // canMove = !canMove;
    }
}
