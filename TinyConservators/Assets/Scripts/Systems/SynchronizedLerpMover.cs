using UnityEngine;

public class SynchronizedLerpMover : MonoBehaviour
{
    [System.Serializable]
    public class LerpTarget
    {
        public Transform objectToMove;
        public Vector3 targetPosition;

        [HideInInspector]
        public Vector3 startPosition;
    }

    public LerpTarget[] targets = new LerpTarget[3];
    public float duration = 2f;

    private float elapsedTime = 0f;
    private bool isMoving = false;

    void Start()
    {
        // Store initial positions
        foreach (var target in targets)
        {
            if (target.objectToMove != null)
            {
                target.startPosition = target.objectToMove.position;
            }
        }
    }

    void Update()
    {
        if (!isMoving) return;

        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / duration);

        foreach (var target in targets)
        {
            if (target.objectToMove != null)
            {
                target.objectToMove.position = Vector3.Lerp(target.startPosition, target.targetPosition, t);
            }
        }

        if (t >= 1f)
        {
            isMoving = false;
        }
    }

    public void StartLerp()
    {
        elapsedTime = 0f;
        isMoving = true;

        // Refresh start positions in case objects have moved
        foreach (var target in targets)
        {
            if (target.objectToMove != null)
            {
                target.startPosition = target.objectToMove.position;
            }
        }
    }
}
