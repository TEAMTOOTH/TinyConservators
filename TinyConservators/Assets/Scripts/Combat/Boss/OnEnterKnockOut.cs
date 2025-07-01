using UnityEngine;

public class OnEnterKnockOut : MonoBehaviour
{
    [SerializeField] string collisionTag;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(collisionTag))
        {
            collision.GetComponent<IKnockoutable>()?.Knockout();
        }
    }
}
