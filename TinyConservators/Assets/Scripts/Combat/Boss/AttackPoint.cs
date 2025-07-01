using System;
using Unity.Cinemachine;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    [SerializeField] Sprite[] visuals;
    [SerializeField] float damagePerStep;
    [SerializeField] GameObject healthBar; //Use the actual bar here, so the green part

    SpriteRenderer visual;
    int damageProgress;
    float damagePercentage = 0;

    float health;

    float damageInterval;

    int damageSteps;

    int totalDamage = 0;

    float damageToPainting = 0;

    int oldDamageIndex = 0;

    Vector3 origHealthBarSize;

    private void Start()
    {
        damageSteps = visuals.Length - 1;
        visual = GetComponent<SpriteRenderer>();

        //damageInterval = visuals.Length;

        origHealthBarSize = healthBar.transform.localScale;

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
        TryToChangeDamageIndex(index);


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
        Debug.Log("DamageIndex is : " + index);
        TryToChangeDamageIndex(index);
        //CalculateHealthBarLook();
        //Damage(damagePercentage);
    }

    private void TryToChangeDamageIndex(int newIndex)
    {
        if(newIndex > oldDamageIndex)
        {
            visual.sprite = visuals[newIndex];
            //Debug.Log("New damage step");
            //Call some sort of negative effect in addition to screen shake.
            CinemachineImpulseSource cm = GetComponent<CinemachineImpulseSource>();
            if(cm != null)
            {
                cm.GenerateImpulseWithForce(0.2f);
                CalculateHealthBarLook();
                //Debug.Log("Shake the screen");
            }

            oldDamageIndex = newIndex;
        }
        else if(newIndex < oldDamageIndex)
        {
            CalculateHealthBarLook();
            //Call some positive effect
            visual.sprite = visuals[newIndex];
            oldDamageIndex = newIndex;
        }
    }

    void CalculateHealthBarLook()
    {
        float healthBarSize = Mathf.Clamp(1 - damageToPainting, 0, origHealthBarSize.x);
        healthBar.transform.localScale = new Vector3(healthBarSize, origHealthBarSize.y, origHealthBarSize.z);
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
