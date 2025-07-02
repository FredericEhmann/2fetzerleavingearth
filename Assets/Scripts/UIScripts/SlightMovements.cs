using UnityEngine;

public class SlightMovements : MonoBehaviour
{
    [Header("Zoom Settings")]
    [Tooltip("Minimum scale for zooming.")]
    public float minScale = 0.80f;

    [Tooltip("Maximum scale for zooming.")]
    public float maxScale = 1.20f;

    [Tooltip("Speed of the zoom effect.")]
    public float zoomSpeed = 0.1f;

    private Vector3 originalScale;
    private float targetScale;
    private float currentLerpTime;
    private bool isZoomingIn;

    void Start()
    {
        // Save the initial scale of the object
        originalScale = transform.localScale;
        targetScale = Random.Range(minScale, maxScale);
        isZoomingIn = true;
    }

    void Update()
    {
        // Smoothly transition to the target scale
        currentLerpTime += Time.deltaTime * zoomSpeed;
        float t = Mathf.SmoothStep(0, 1, currentLerpTime);

        if (isZoomingIn)
        {
            transform.localScale = Vector3.Lerp(originalScale, originalScale * targetScale, t);
        }
        else
        {
            transform.localScale = Vector3.Lerp(originalScale * targetScale, originalScale, t);
        }

        // If the transition is complete, set up the next target
        if (currentLerpTime >= 1.0f)
        {
            currentLerpTime = 0f;
            isZoomingIn = !isZoomingIn;
            if (isZoomingIn)
            {
                targetScale = Random.Range(minScale, maxScale);
            }
        }
    }
}