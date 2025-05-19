using UnityEngine;

public class BossDamage : MonoBehaviour
{
    private void Start()
    {
        AllowCollisions(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goblin"))
        {
            if(collision.GetComponent<Minion>().State == MinionStates.projectile)
            {
                GetComponentInParent<Boss>().State = BossStates.hurt;
                
            }
        }
    }

    //Why is this here?
    public void AllowCollisions(bool state)
    {
        GetComponent<CapsuleCollider2D>().enabled = state;
    }
}
