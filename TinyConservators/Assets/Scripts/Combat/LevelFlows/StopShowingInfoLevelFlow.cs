using System.Collections;
using UnityEngine;

public class StopShowingInfoLevelFlow : MonoBehaviour, ILevelFlowComponent
{
    [SerializeField] GameObject objectToHide;
    [SerializeField] bool fadeOut;
    [SerializeField] float fadeOutTime;

    LevelFlowManager owner;

    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;

        if (!fadeOut)
        {
            objectToHide.SetActive(false);
        }
        else
        {
            SpriteRenderer spriteRenderer = objectToHide.GetComponent<SpriteRenderer>();
            StartCoroutine(FadeOut());
            IEnumerator FadeOut()
            {
                Color originalColor = spriteRenderer.color;
                float startAlpha = originalColor.a;
                float time = 0f;

                while (time < fadeOutTime)
                {
                    float alpha = Mathf.Lerp(startAlpha, 0f, time / fadeOutTime);
                    spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                    time += Time.deltaTime;
                    yield return null;
                }

                // Ensure alpha is fully 0
                spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
            }
        }

        FinishSection();
    }
}