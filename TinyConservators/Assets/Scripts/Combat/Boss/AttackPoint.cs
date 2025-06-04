using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    [SerializeField] Sprite[] visuals;

    SpriteRenderer visual;
    int damageProgress;
    float damagePercentage = 0;

    float damageInterval;

    private void Start()
    {
        visual = GetComponent<SpriteRenderer>();

        damageInterval = visuals.Length;

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
        damagePercentage = Mathf.Clamp01(percentageOfDamage); // Ensure it's between 0 and 1

        int index = Mathf.FloorToInt(damagePercentage * damageInterval);
        index = Mathf.Clamp(index, 0, visuals.Length - 1); // Prevent out-of-range

        visual.sprite = visuals[index];
    }

    public void FixDamage(float amount)
    {
        damagePercentage -= amount;
        if(amount < 0)
        {
            amount = 0;
        }
        Damage(damagePercentage);
    }

    public int GetAmountOfVisualDamageSteps()
    {
        return visuals.Length;
    }

    public float GetAmountOfDamage()
    {
        return damagePercentage;
    }

}
