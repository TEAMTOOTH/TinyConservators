using System.Collections;
using UnityEngine;

public class SpiralMover : MonoBehaviour, IVisualMove
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float duration = 5f;
    [SerializeField] private float spiralGrowth = 1f;
    [SerializeField] private float spiralFrequency = 4f;
    [SerializeField] private Vector3 targetScale = Vector3.one;
    [SerializeField] private bool reverse = false;

    private float elapsedTime = 0f;
    private Vector3 initialScale;
    private Vector3 initialPosition;

    private Vector3 finalPosition;

    private void Start()
    {
        
        /*if(startPoint == null)
        {
            initialPosition = transform.position;
        }
        else
        {
            transform.position = startPoint.position;
            initialPosition = startPoint.position;
        }

        initialScale = transform.localScale;
        finalPosition = endPoint.position;*/
    }

    private void Update()
    {
        /*
        if (elapsedTime >= duration)
            return;

        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / duration);

        // Adjust t for reverse mode
        float progress = reverse ? 1f - t : t;

        // Base interpolation
        Vector3 from = reverse ? endPoint.position : startPoint.position;
        Vector3 to = reverse ? startPoint.position : endPoint.position;

        Vector3 linearPos = Vector3.Lerp(from, to, progress);

        // Spiral offset (only in X and Y)
        float angle = progress * Mathf.PI * 2f * spiralFrequency;
        float radius = progress * spiralGrowth;
        float offsetX = Mathf.Cos(angle) * radius;
        float offsetY = Mathf.Sin(angle) * radius;

        Vector3 spiralPos = new Vector3(linearPos.x + offsetX, linearPos.y + offsetY, linearPos.z);
        transform.position = spiralPos;

        // Scale interpolation
        Vector3 fromScale = reverse ? targetScale : initialScale;
        Vector3 toScale = reverse ? initialScale : targetScale;
        transform.localScale = Vector3.Lerp(fromScale, toScale, progress);
        */
    }

    // Optional: call this to reset/restart motion
    public void Restart()
    {
        elapsedTime = 0f;
    }

    /*public void Move(float time)
    {
        // Always reset the timer
        elapsedTime = 0f;

        // Determine direction and setup
        if (reverse)
        {
            initialPosition = endPoint != null ? endPoint.position : transform.position;
            finalPosition = startPoint != null ? startPoint.position : transform.position;
        }
        else
        {
            initialPosition = startPoint != null ? startPoint.position : transform.position;
            finalPosition = endPoint != null ? endPoint.position : transform.position;
        }

        transform.position = initialPosition;
        initialScale = transform.localScale;

        StartCoroutine(move());

        IEnumerator move()
        {
            while (elapsedTime < time)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / time);

                // Adjust progress depending on direction
                float progress = reverse ? 1f - t : t;

                // Interpolate position
                Vector3 linearPos = Vector3.Lerp(initialPosition, finalPosition, progress);

                // Spiral offset in X/Y only
                float angle = progress * Mathf.PI * 2f * spiralFrequency;
                float radius = progress * spiralGrowth;
                float offsetX = Mathf.Cos(angle) * radius;
                float offsetY = Mathf.Sin(angle) * radius;

                Vector3 spiralPos = new Vector3(linearPos.x + offsetX, linearPos.y + offsetY, linearPos.z);
                transform.position = spiralPos;

                // Interpolate scale
                Vector3 fromScale = reverse ? targetScale : initialScale;
                Vector3 toScale = reverse ? initialScale : targetScale;
                transform.localScale = Vector3.Lerp(fromScale, toScale, progress);

                yield return null;
            }

            // Ensure final values are set exactly
            transform.position = finalPosition;
            transform.localScale = reverse ? initialScale : targetScale;
        }
    }*/

    public void Move(float time)
    {
        elapsedTime = 0f;

        if (reverse)
        {
            initialPosition = endPoint != null ? endPoint.position : transform.position;
            finalPosition = startPoint != null ? startPoint.position : transform.position;
        }
        else
        {
            initialPosition = startPoint != null ? startPoint.position : transform.position;
            finalPosition = endPoint != null ? endPoint.position : transform.position;
        }

        transform.position = initialPosition;
        initialScale = transform.localScale;

        StartCoroutine(move());

        IEnumerator move()
        {
            while (elapsedTime < time)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / time);

                float progress = reverse ? 1f - t : t;

                // Interpolate base position
                Vector3 linearPos = Vector3.Lerp(initialPosition, finalPosition, progress);

                // Spiral offset that fades out as it approaches the destination
                float angle = progress * Mathf.PI * 2f * spiralFrequency;
                float radius = progress * spiralGrowth * (1f - progress); // Shrinks to 0 at end
                float offsetX = Mathf.Cos(angle) * radius;
                float offsetY = Mathf.Sin(angle) * radius;

                Vector3 spiralPos = new Vector3(linearPos.x + offsetX, linearPos.y + offsetY, linearPos.z);
                transform.position = spiralPos;

                // Interpolate scale
                Vector3 fromScale = reverse ? targetScale : initialScale;
                Vector3 toScale = reverse ? initialScale : targetScale;
                transform.localScale = Vector3.Lerp(fromScale, toScale, progress);

                yield return null;
            }

            // Final cleanup to ensure exact match
            transform.position = finalPosition;
            transform.localScale = reverse ? initialScale : targetScale;
        }
    }
}
