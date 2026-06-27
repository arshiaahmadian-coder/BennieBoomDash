using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float xOfset;

    private void FixedUpdate()
    {
        transform.position = new Vector3(
            targetTransform.position.x - xOfset, transform.position.y, transform.position.z);
    }
}
