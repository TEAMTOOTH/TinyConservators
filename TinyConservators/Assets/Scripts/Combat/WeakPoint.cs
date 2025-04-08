using UnityEngine;

public class WeakPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IDamageReceiver damage = GetComponentInParent<IDamageReceiver>();
            damage.Hurt();
        }
        
    }
}
