using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class VisualTurnOffCollider : MonoBehaviour
{
    public bool shouldBeEnabled { get; set; } = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && shouldBeEnabled)
        {
            //collision.GetComponent<Player>().ShowVisual(false);
            collision.GetComponent<Player>().FullFreeze(true);
            collision.transform.position = new Vector3(0, 100, 0);
        }
    }
}
