using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    [SerializeField] Sprite[] visuals;

    SpriteRenderer visual;
    int damageProgress;

    private void Start()
    {
        visual = GetComponent<SpriteRenderer>();
    }

    public void ProgressVisual()
    {
        damageProgress++;
        if(damageProgress < visuals.Length)
        {
            visual.sprite = visuals[damageProgress];
        }

    }

    public int GetAmountOfVisualDamageSteps()
    {
        return visuals.Length;
    }

}
