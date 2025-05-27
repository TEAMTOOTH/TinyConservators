using UnityEngine;

public class ProtectionItem : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IKnockoutable knockout =  collision.GetComponent<IKnockoutable>();
        if(knockout != null)
        {
            if (!knockout.IsKnockedOut())
            {
                knockout.Knockout();
            }
        }
    }
}
