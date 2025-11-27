using UnityEngine;

public class BossAttackOverlord : MonoBehaviour
{
    //This is some of the worst code I have ever written...
    BossItemManagerManager[] attacks;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attacks = GetComponentsInChildren<BossItemManagerManager>();
    }

    public void Attack(int attackIndex)
    {
        if(attackIndex < attacks.Length)
        {
            attacks[attackIndex].Attack();
        }
    }

    public void Despawn(int attackIndex)
    {
        if (attackIndex < attacks.Length)
        {
            attacks[attackIndex].Despawn();
        }
    }
}
