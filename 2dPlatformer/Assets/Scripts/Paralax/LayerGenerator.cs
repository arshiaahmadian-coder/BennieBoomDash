using Unity.VisualScripting;
using UnityEngine;

public class LayerGenerator : MonoBehaviour
{
    private Transform cameraTransform;
    [SerializeField] float imageWidth;

    private void Start()
    {
        cameraTransform = FindFirstObjectByType<ParallaxCamera>().transform;
    }

    private void Update()
    {
        if (cameraTransform.position.x >= transform.position.x + imageWidth)
        {
            transform.position = new Vector3(transform.position.x + imageWidth, transform.position.y,
            transform.position.z);
        }
    }
}
