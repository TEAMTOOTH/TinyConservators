using UnityEngine;

public class Eat : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IEatable eatObject = collision.GetComponent<IEatable>();
        if(eatObject != null)
        {
            eatObject.Eat(transform.parent.gameObject);
        }
    }
}
