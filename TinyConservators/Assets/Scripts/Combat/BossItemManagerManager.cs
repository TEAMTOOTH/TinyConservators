using UnityEngine;

public class BossItemManagerManager : MonoBehaviour
{
    BossItemManager[] attacks;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attacks = GetComponents<BossItemManager>();
        Debug.Log("Attack length is: " + attacks.Length);
    }

    public void Attack()
    {
        Debug.Log("Attack is on object: " + gameObject.name);
        for(int i = 0; i < attacks.Length; i++)
        {
            attacks[i].SpawnObjects();
        }
    }

    public void Despawn()
    {
        for (int i = 0; i < attacks.Length; i++)
        {
            attacks[i].DespawnObjects();
        }
    }
}
