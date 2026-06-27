using UnityEngine;

[ExecuteInEditMode]
public class ParallaxCamera : MonoBehaviour
{
    public delegate void ParallaxCameraDelegate(Vector2 deltaMovement);
    public ParallaxCameraDelegate onCameraTranslate;

    private Vector3 oldPosition;

    void Start()
    {
        oldPosition = transform.position;
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;

        if (currentPosition != oldPosition)
        {
            if (onCameraTranslate != null)
            {
                Vector2 delta = new Vector2(oldPosition.x - currentPosition.x, oldPosition.y - currentPosition.y);
                onCameraTranslate(delta);
            }

            oldPosition = currentPosition;
        }
    }
}
