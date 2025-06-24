using UnityEngine;

public class RadialPush2D : MonoBehaviour
{
    [Header("Push Settings")]
    [SerializeField] private float radius = 5f;
    [SerializeField] private float force = 10f;
    [SerializeField] private LayerMask affectedLayers;

    public void PushAway()
    {
        // Get all colliders within radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, affectedLayers);

        foreach (Collider2D col in colliders)
        {
            Rigidbody2D rb = col.attachedRigidbody;
            if (rb != null && rb != GetComponent<Rigidbody2D>())
            {
                rb.GetComponent<IKnockoutable>().Knockout();
                Vector2 direction = (rb.position - (Vector2)transform.position).normalized;
                rb.AddForce(direction * force, ForceMode2D.Impulse);
            }
        }
    }

    // Optional: trigger via key for testing
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //PushAway();
        }
    }

    // Optional: visualize the effect radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
