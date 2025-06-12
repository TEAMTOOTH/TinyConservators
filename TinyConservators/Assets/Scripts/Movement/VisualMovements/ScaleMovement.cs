using System.Collections;
using UnityEngine;

public class ScaleMovement : MonoBehaviour, IVisualMove
{
    [SerializeField] private Vector3 endScale;

    private Vector3 startScale;

    public void Move(float totalTime)
    {
        StartCoroutine(MoveCoroutine(totalTime));
    }

    private IEnumerator MoveCoroutine(float totalTime)
    {
        startScale = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < totalTime)
        {
            elapsedTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / totalTime);
            yield return null;
        }

        transform.localScale = endScale; // Ensure final position is exact
    }


}
