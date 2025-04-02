using UnityEngine;
using UnityEngine.Events;

public class FeetCollider : MonoBehaviour
{
    public UnityEvent land;

    private void OnEnable()
    {
        land.AddListener(GetComponentInParent<FlyingMovement>().ResetFlaps);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            land.Invoke();
        }
    }

    private void OnDisable()
    {
        //Always close open memory leaks!
        land.RemoveAllListeners();
    }
}
