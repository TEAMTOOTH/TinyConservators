using UnityEngine;
using System.Collections;

public class StraighLineMovement : MonoBehaviour, IVisualMove
{
    
    [SerializeField] private Vector3 endPosition;

    private Vector3 startPosition;

    public void Move(float totalTime)
    {
        StartCoroutine(MoveCoroutine(totalTime));
    }

    private IEnumerator MoveCoroutine(float totalTime)
    {
        startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < totalTime)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / totalTime);
            yield return null;
        }

        transform.position = endPosition; // Ensure final position is exact
    }
}
