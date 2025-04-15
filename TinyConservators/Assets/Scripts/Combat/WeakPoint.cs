using UnityEngine;

public class WeakPoint : MonoBehaviour
{
    [SerializeField] string enemyTag;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(enemyTag))
        {
            Debug.Log($"KnockedOut by {collision.tag} in weakpoint");
            IDamageReceiver damage = GetComponentInParent<IDamageReceiver>();
            damage.Hurt();
        }
        
    }
}
