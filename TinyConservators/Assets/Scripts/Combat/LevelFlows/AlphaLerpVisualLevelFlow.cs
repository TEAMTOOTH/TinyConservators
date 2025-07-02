using System.Collections;
using UnityEngine;

public class AlphaLerpVisualLevelFlow : MonoBehaviour, ILevelFlowComponent
{
    [SerializeField] private GameObject targetObject; // GameObject with the SpriteRenderer(s)
    [SerializeField, Range(0f, 1f)] private float targetAlpha = 0.5f;
    [SerializeField] private float duration = 1f;
    [SerializeField] private bool parallel = false;

    private LevelFlowManager owner;

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;

        // Start the alpha lerp
        StartCoroutine(LerpAlpha());

        if (parallel)
        {
            FinishSection(); // flow continues immediately
        }
    }

    public void FinishSection()
    {
        owner?.ProgressFlow();
    }

    private IEnumerator LerpAlpha()
    {
        SpriteRenderer[] spriteRenderers = targetObject.GetComponentsInChildren<SpriteRenderer>();
        float timeElapsed = 0f;

        // Store initial alphas
        Color[] originalColors = new Color[spriteRenderers.Length];
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            originalColors[i] = spriteRenderers[i].color;
        }

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;

            for (int i = 0; i < spriteRenderers.Length; i++)
            {
                Color c = originalColors[i];
                float newAlpha = Mathf.Lerp(c.a, targetAlpha, t);
                spriteRenderers[i].color = new Color(c.r, c.g, c.b, newAlpha);
            }

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Set final alpha exactly
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            Color c = spriteRenderers[i].color;
            c.a = targetAlpha;
            spriteRenderers[i].color = c;
        }

        if (!parallel)
        {
            FinishSection(); // flow continues after fade finishes
        }
    }
}
