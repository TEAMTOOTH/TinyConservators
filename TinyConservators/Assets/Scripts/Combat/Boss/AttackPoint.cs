using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    [SerializeField] Sprite[] visuals;
    [SerializeField] float damagePerStep;

    SpriteRenderer visual;
    int damageProgress;
    float damagePercentage = 0;

    float health;

    float damageInterval;

    int damageSteps = 6;

    int totalDamage = 0;

    float damageToPainting = 0;

    private void Start()
    {
        visual = GetComponent<SpriteRenderer>();

        damageInterval = visuals.Length;

        

    }

    public void NewAttack()
    {
        totalDamage = 0;
    }

    public void ProgressVisual()
    {
        damageProgress++;
        if(damageProgress < visuals.Length)
        {
            visual.sprite = visuals[damageProgress];
        }

    }

    public void Damage(float percentageOfDamage)
    {
        int damageStep = Mathf.FloorToInt(percentageOfDamage * 10);

        //Debug.Log("total damage = " + totalDamage + ", damageStep" + damageStep);
        if(totalDamage != damageStep)
        {
            totalDamage = damageStep;
            DamagePainting();
        }
        

        /*
        // Clamp input between 0–1
        percentageOfDamage = Mathf.Clamp01(percentageOfDamage);

        // Convert percentage into step-based damage
        float stepSize = 1f / damageSteps;

        // Multiply by percentage of full damage to apply steps
        float damageToAdd = percentageOfDamage * damageSteps * stepSize;

        // Add damage, clamped to 1
        damagePercentage = Mathf.Min(damagePercentage + damageToAdd, 1f);

        Debug.Log(damagePercentage);
        // Optional: drive visuals
        // int index = Mathf.FloorToInt(damagePercentage * damageSteps);
        // index = Mathf.Clamp(index, 0, visuals.Length - 1);
        // visual.sprite = visuals[index];

        // Debug.Log($"Damage %: {damagePercentage}");*/
    }

    void DamagePainting()
    {
        damageToPainting +=  damagePerStep;
        //damageToPainting += .1f;

        int index = Mathf.FloorToInt(damageToPainting * damageSteps);
        index = Mathf.Clamp(index, 0, visuals.Length - 1);
        visual.sprite = visuals[index];
        
    }

    public void FixDamage(float amount)
    {
        damageToPainting -= amount;
        
        if(damageToPainting < 0)
        {
            damageToPainting = 0;
        }

        int index = Mathf.FloorToInt(damageToPainting * damageSteps);
        index = Mathf.Clamp(index, 0, visuals.Length - 1);
        visual.sprite = visuals[index];
        //Damage(damagePercentage);
    }

    public int GetAmountOfVisualDamageSteps()
    {
        return visuals.Length;
    }

    public float GetAmountOfDamage()
    {
        return damageToPainting;
    }

}
