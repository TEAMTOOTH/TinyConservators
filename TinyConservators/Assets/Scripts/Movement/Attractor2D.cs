using System.Collections;
using UnityEngine;

public class Attractor2D : MonoBehaviour, IVisualMove
{
    [SerializeField] float attractionForce = 10f;
    [SerializeField] Vector2 attractionBoxSize = new Vector2(5f, 5f);
    [SerializeField] LayerMask affectedLayers;
    [SerializeField] bool attract = false;

    [Tooltip("Defines how force falls off from center (x = 0 = close, x = 1 = edge).")]
    [SerializeField] AnimationCurve falloffCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);

    void FixedUpdate()
    {
        if (!attract)
            return;

        // Get all colliders in a box-shaped area
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, attractionBoxSize, 0f, affectedLayers);

        foreach (var col in colliders)
        {
            Rigidbody2D rb = col.attachedRigidbody;
            if (rb != null && rb.gameObject != this.gameObject)
            {
                Vector2 toCenter = (Vector2)transform.position - rb.position;
                Vector2 normalized = toCenter.normalized;

                // Compute normalized distance from attractor to object (0 = center, 1 = edge)
                Vector2 localOffset = transform.InverseTransformVector(rb.position - (Vector2)transform.position);
                float dx = Mathf.Abs(localOffset.x) / (attractionBoxSize.x * 0.5f);
                float dy = Mathf.Abs(localOffset.y) / (attractionBoxSize.y * 0.5f);
                float t = Mathf.Clamp01(Mathf.Max(dx, dy));  // max to match the farthest axis

                float falloff = falloffCurve.Evaluate(t);
                float force = attractionForce * falloff;

                rb.AddForce(normalized * force);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, attractionBoxSize);
    }

    public void Move(float time)
    {
        StartCoroutine(SetAttract());
        IEnumerator SetAttract()
        {
            attract = true;
            yield return new WaitForSeconds(time);
            attract = false;
        }
    }
}