using UnityEngine;

public class WeakPoint : MonoBehaviour
{
    [SerializeField] string[] enemyTags;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CompareTags(collision.tag))
        {
            //Debug.Log($"KnockedOut by {collision.tag} in weakpoint");
            IDamageReceiver damage = GetComponentInParent<IDamageReceiver>();
            damage.Hurt();
        }
    }

    private bool CompareTags(string tagToCompare)
    {
        for(int i = 0; i < enemyTags.Length; i++)
        {
            if (tagToCompare.Equals(enemyTags[i]))
            {
                return true;
            }
        }
        return false;
    }
}
