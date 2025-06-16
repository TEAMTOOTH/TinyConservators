using UnityEngine;

public class TouchKnockout : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IKnockoutable knockout = collision.gameObject.GetComponent<IKnockoutable>();

        if(knockout != null)
        {
            if (!knockout.IsKnockedOut())
            {
                IDamageReceiver damage = collision.gameObject.GetComponent<IDamageReceiver>();
                if(damage != null)
                {
                    damage.Hurt(gameObject);
                }
            }
        }
    }
}
