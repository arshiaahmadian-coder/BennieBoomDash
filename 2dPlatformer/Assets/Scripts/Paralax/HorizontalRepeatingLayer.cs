using UnityEngine;
using System.Collections.Generic;

[ExecuteAlways]
public class HorizontalRepeatingLayer : MonoBehaviour
{
    [Header("References")]
    public Transform cameraTransform;

    [Header("Settings")]
    public int visibleCountPerSide = 2;

    public float spacingOffset = 0f;

    private float spriteWidth;
    private List<Transform> pieces = new List<Transform>();
    private Vector3 lastCamPos;

    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        Init();
        lastCamPos = cameraTransform.position;
    }

    void Init()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        pieces.Clear();

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        spriteWidth = sr.bounds.size.x;

        pieces.Add(transform);

        int total = visibleCountPerSide * 2;

        for (int i = 1; i <= total; i++)
        {
            Transform clone = Instantiate(transform, transform.parent);
            clone.name = name + "_Clone_" + i;
            clone.position = transform.position + Vector3.right * i * (spriteWidth + spacingOffset);
            pieces.Add(clone);
        }
    }

    void LateUpdate()
    {
        float camDeltaX = cameraTransform.position.x - lastCamPos.x;

        foreach (Transform t in pieces)
        {
            t.position += Vector3.right * camDeltaX;
        }

        Reposition();
        lastCamPos = cameraTransform.position;
    }

    void Reposition()
    {
        float camX = cameraTransform.position.x;
        float fullWidth = spriteWidth + spacingOffset;

        foreach (Transform t in pieces)
        {
            float diff = t.position.x - camX;

            if (diff > fullWidth * visibleCountPerSide)
            {
                t.position -= Vector3.right * fullWidth * pieces.Count;
            }
            else if (diff < -fullWidth * visibleCountPerSide)
            {
                t.position += Vector3.right * fullWidth * pieces.Count;
            }
        }
    }
}
