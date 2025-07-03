using System.Collections;
using UnityEngine;

public class PyramidColorSwitch : MonoBehaviour
{

    [SerializeField] SpriteRenderer [] spriteRenderer;
    [SerializeField] Material edge;
    Color[] colorPalette = { Color.red, Color.green, Color.blue, Color.yellow, Color.magenta };
    Color currentColor;
    Color targetColor;
    Color emissionColor;

    float lerpSpeed = 0.5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetCurrentColor();
    }

    // Update is called once per frame
    void Update()
    {


    }


    void ChangeColor()
    {

        SpriteRenderer[] spriteRenderers = spriteRenderer;

        foreach (SpriteRenderer sr in spriteRenderers)
        {
            sr.color = currentColor;
        }

        edge.color = currentColor;
        // Enable emission keyword
        edge.EnableKeyword("_EMISSION");

        // Set the emission color
        edge.SetColor("_EmissionColor", emissionColor);

    }

    void SetCurrentColor()
    {
        Color randomColor = colorPalette[Random.Range(0, colorPalette.Length)];
        currentColor = randomColor;
        emissionColor = currentColor;

        StartCoroutine(ChangeColorTimer());

        IEnumerator ChangeColorTimer()
        {
            yield return new WaitForSeconds(1);
            ChangeColor();
            SetCurrentColor();
        }
    }

    void SetTargetColor()
    {
        Color randomColor = colorPalette[Random.Range(0, colorPalette.Length)];
        targetColor = randomColor;
    }
}
