using System.Collections;
using UnityEngine;

public class ColorSyncer : MonoBehaviour
{
    private SpriteRenderer[] spriteRenderers;
    private Renderer parentRenderer;

    public float interval = 1f;

    void Start()
    {
        // Get all SpriteRenderers in children (including possibly the parent)
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        // Get the parent's Renderer (MeshRenderer, SpriteRenderer, etc.)
        parentRenderer = GetComponent<Renderer>();

        // Start the repeating color change
        StartCoroutine(ChangeColorLoop());
    }

    IEnumerator ChangeColorLoop()
    {
        while (true)
        {
            // Generate a new random color
            Color randomColor = new Color(Random.value, Random.value, Random.value);

            // Apply to all SpriteRenderers
            foreach (var sr in spriteRenderers)
            {
                sr.color = randomColor;
            }

            // Apply to the parent's material (if it has one)
            if (parentRenderer != null && parentRenderer.material != null)
            {
                Material mat = parentRenderer.material;

                // Set base color
                mat.color = randomColor;

                // Enable and set emission color
                mat.EnableKeyword("_EMISSION");
                mat.SetColor("_EmissionColor", randomColor);
            }

            yield return new WaitForSeconds(interval);
        }
    }
}
