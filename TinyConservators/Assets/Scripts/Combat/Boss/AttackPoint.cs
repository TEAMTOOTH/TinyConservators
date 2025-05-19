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

    public int GetAmountOfVisualDamageSteps()
    {
        return visuals.Length;
    }

}
