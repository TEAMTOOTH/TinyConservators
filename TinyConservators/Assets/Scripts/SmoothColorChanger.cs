using System.Collections;
using UnityEngine;

public class SmoothColorChanger : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] spriteRenderer;
    [SerializeField] Material edge;

    Color[] colorPalette = { Color.red, Color.green, Color.blue, Color.yellow, Color.magenta };

    Color currentColor;
    Color targetColor;
    Color emissionColor;

    float lerpSpeed = .5f; // higher = faster transition

    void Start()
    {
        // Set initial color
        currentColor = GetRandomColor();
        targetColor = currentColor;
        emissionColor = currentColor;

        StartCoroutine(ChangeTargetColorRoutine());
    }

    void Update()
    {
        // Lerp from current to target color
        currentColor = Color.Lerp(currentColor, targetColor, Time.deltaTime * lerpSpeed);
        emissionColor = currentColor;

        // Apply current color smoothly every frame
        ApplyColor();
    }

    void ApplyColor()
    {
        foreach (var sr in spriteRenderer)
        {
            sr.color = currentColor;
        }

        if (edge != null)
        {
            edge.color = currentColor;
            edge.EnableKeyword("_EMISSION");
            edge.SetColor("_EmissionColor", emissionColor); // boost emission
        }
    }

    IEnumerator ChangeTargetColorRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // Change color every 1 sec
            targetColor = GetRandomColor();
        }
    }

    Color GetRandomColor()
    {
        return colorPalette[Random.Range(0, colorPalette.Length)];
    }
}
