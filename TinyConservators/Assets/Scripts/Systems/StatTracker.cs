using UnityEngine;

public class StatTracker : MonoBehaviour
{
    float[] damagePercentages;
    private void Awake()
    {
        damagePercentages = new float[3];
        DontDestroyOnLoad(gameObject);
    }
    
    public void SetStats(int levelNumber)
    {
        GameObject[] g = GameObject.FindGameObjectsWithTag("AttackPoint");

        Debug.Log("Attackpoints" + g.Length);

        for(int i = 0; i < g.Length; i++)
        {
            damagePercentages[levelNumber] += g[i].GetComponent<AttackPoint>().GetAmountOfDamage();
        }
    }

    public float GetDamagePercentage(int levelNumber)
    {
        return damagePercentages[levelNumber];
    }

}
