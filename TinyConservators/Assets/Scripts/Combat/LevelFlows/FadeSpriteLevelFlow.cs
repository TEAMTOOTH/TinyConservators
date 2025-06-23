using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeSpriteLevelFlow : MonoBehaviour, ILevelFlowComponent
{
    [SerializeField] private GameObject objectToFade;
    [SerializeField] private bool fadeIn = true;              // Toggle this to fade in/out
    [SerializeField] private float fadeDuration = 1f;         // Duration of fade in seconds
    [SerializeField] private bool parallell;         

    private LevelFlowManager owner;

    // Delegates for abstracting get/set color
    private Func<Color> getColor;
    private Action<Color> setColor;

    private void Awake()
    {
        if (objectToFade == null)
        {
            Debug.LogError("FadeSpriteLevelFlow: objectToFade is not assigned.");
            enabled = false;
            return;
        }

        // Detect compatible component and assign color handlers
        if (objectToFade.TryGetComponent(out SpriteRenderer sr))
        {
            getColor = () => sr.color;
            setColor = c => sr.color = c;
        }
        else if (objectToFade.TryGetComponent(out TMP_Text tmp))
        {
            getColor = () => tmp.color;
            setColor = c => tmp.color = c;
        }
        else if (objectToFade.TryGetComponent(out Image img))
        {
            getColor = () => img.color;
            setColor = c => img.color = c;
        }
        else
        {
            Debug.LogError("FadeSpriteLevelFlow: No supported component with a color property found on objectToFade.");
            enabled = false;
        }
    }

    public void StartFade(bool fadeToVisible)
    {
        StartCoroutine(FadeCoroutine(fadeToVisible));
    }

    private IEnumerator FadeCoroutine(bool fadeToVisible)
    {
        float startAlpha = getColor().a;
        float endAlpha = fadeToVisible ? 1f : 0f;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {

            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, t);

            Color c = getColor();
            c.a = newAlpha;
            setColor(c);

            yield return null;
        }

        // Ensure exact end alpha
        Color finalColor = getColor();
        finalColor.a = endAlpha;
        setColor(finalColor);

        if (!parallell)
        {
            FinishSection();
        }
    }

    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;
        StartFade(fadeIn);
        if (parallell)
        {
            FinishSection();
        }
    }
}
