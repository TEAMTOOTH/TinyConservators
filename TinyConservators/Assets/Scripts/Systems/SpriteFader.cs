using UnityEngine;
using System.Collections;

public class SpriteFader : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Coroutine fadeCoroutine;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteFader requires a SpriteRenderer component!");
        }
    }

    /// <summary>
    /// Fades the sprite's alpha from its current value to the target value over time.
    /// </summary>
    /// <param name="targetAlpha">The alpha to fade to (0 = invisible, 1 = fully visible).</param>
    /// <param name="duration">The time over which to fade.</param>
    public void FadeTo(float targetAlpha, float duration)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeCoroutine(targetAlpha, duration));
    }

    private IEnumerator FadeCoroutine(float targetAlpha, float duration)
    {
        Color startColor = spriteRenderer.color;
        float startAlpha = startColor.a;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / duration);
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
            yield return null;
        }

        // Ensure it ends exactly at the target alpha
        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
        fadeCoroutine = null;
    }
}
