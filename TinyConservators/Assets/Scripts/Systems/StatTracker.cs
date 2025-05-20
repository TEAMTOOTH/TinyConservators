using UnityEngine;

public class StatTracker : MonoBehaviour
{
    float damagePercentage = 0;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    public void SetStats()
    {
        GameObject[] g = GameObject.FindGameObjectsWithTag("AttackPoint");
        
        for(int i = 0; i < g.Length; i++)
        {
            damagePercentage += g[i].GetComponent<AttackPoint>().GetAmountOfDamage() / g.Length;
        }
    }

    public float GetDamagePercentage()
    {
        return damagePercentage;
    }

}
