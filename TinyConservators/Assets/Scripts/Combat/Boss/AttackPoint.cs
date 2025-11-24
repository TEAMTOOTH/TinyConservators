using System;
using Unity.Cinemachine;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    [SerializeField] Sprite[] visuals;
    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject fixParticleSystem;

    SpriteRenderer visual;
    int damageSteps; // total steps of visible damage

    float damageToPainting = 0f; // between 0-1
    int oldDamageIndex = 0;

    int damageIndex = 0;

    float hackyFixAmount = 0;

    Vector3 origHealthBarSize;

    private void Start()
    {
        visual = GetComponent<SpriteRenderer>();
        origHealthBarSize = healthBar.transform.localScale;
        damageSteps = visuals.Length - 1;
        //UpdateVisuals();
        //CalculateHealthBarLook();
    }

    public void NewAttack()
    {
        damageToPainting = 0;
        oldDamageIndex = 0;
        //UpdateVisuals();
        //CalculateHealthBarLook();
    }

    public void Damage()
    {
        damageIndex++;
        
        UpdateVisuals();
        CalculateHealthBarLook();
    }

    public void Damage(int damageSteps)
    {
        damageIndex += damageSteps;

        UpdateVisuals();
        CalculateHealthBarLook();
    }

    void UpdateVisuals()
    {
        // Determine step index based on total damage (0-1 range mapped to steps)
        //int newIndex = Mathf.FloorToInt(damageToPainting * damageSteps);
        //newIndex = Mathf.Clamp(newIndex, 0, visuals.Length - 1);
        int newIndex = damageIndex;

        if (newIndex > oldDamageIndex)
        {
            visual.sprite = visuals[newIndex];

            CinemachineImpulseSource cm = GetComponent<CinemachineImpulseSource>();
            if (cm != null)
            {
                cm.GenerateImpulseWithForce(0.2f);
            }

            oldDamageIndex = newIndex;
        }
        else if(newIndex < oldDamageIndex)
        {
            visual.sprite = visuals[newIndex];
            fixParticleSystem.GetComponent<ParticleSystem>()?.Play();
            //Do a little sparkle
        }
    }

    void CalculateHealthBarLook()
    {
        //float scaleX = Mathf.Clamp(1f - damageToPainting, 0f, 1f);
        //float scaleX = Mathf.Clamp01(damageIndex / damageSteps);
        //Debug.Log(scaleX);
        //healthBar.transform.localScale = new Vector3(scaleX * origHealthBarSize.x, origHealthBarSize.y, origHealthBarSize.z);

        float scaleX = 1f - Mathf.Clamp01((float)damageIndex / damageSteps);

        healthBar.transform.localScale = new Vector3(
            scaleX * origHealthBarSize.x,
            origHealthBarSize.y,
            origHealthBarSize.z
            );

    }

    //Doing a hacky fix, where each painting dot will heal 1 level
    public void FixDamage(float amount)
    {
        hackyFixAmount += amount;
        
        if(hackyFixAmount >= 1.3f)
        {
            if(damageIndex > 0)
            {
                damageIndex--;
            }
            

            UpdateVisuals();
            CalculateHealthBarLook();

            hackyFixAmount = 0;
        }
        //damageIndex--;
        
    }

    /*public void FixDamage(float amount)
    {
        damageToPainting -= amount;
        damageToPainting = Mathf.Clamp01(damageToPainting);
        //damageIndex--;
        UpdateVisuals();
        CalculateHealthBarLook();
    }*/

    public int GetAmountOfVisualDamageSteps()
    {
        return visuals.Length;
    }

    public float GetAmountOfDamage()
    {
        return Mathf.Clamp01((float)damageIndex / damageSteps);
    }

}
