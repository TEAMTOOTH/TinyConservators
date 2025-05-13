using UnityEngine;

public class BossDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goblin"))
        {
            GetComponentInParent<Boss>().State = BossStates.hurt;
        }
    }

    public void AllowCollisions(bool state)
    {
        GetComponent<CapsuleCollider2D>().enabled = state;
    }
}
