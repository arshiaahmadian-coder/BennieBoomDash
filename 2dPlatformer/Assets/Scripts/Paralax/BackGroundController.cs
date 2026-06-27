using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    private float startPos, length;
    [SerializeField] private GameObject cam;
    [SerializeField] float parallaxEffect; // 0 = move with cam | 1 = won't move

    private void Start()
    {
        // cam = Camera.main.gameObject;
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float distance = cam.transform.position.x * parallaxEffect;
        float movement = cam.transform.position.x * (1 - parallaxEffect);

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (movement > startPos + length)
        {
            startPos += length;
        } else if (movement < startPos - length)
        {
            startPos -= length;
        }
    }
}
