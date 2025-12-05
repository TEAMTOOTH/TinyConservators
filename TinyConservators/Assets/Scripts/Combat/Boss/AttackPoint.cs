using System;
using Unity.Cinemachine;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    [Header("Visuals & Health")]
    [SerializeField] Sprite[] visuals;               // 0 = undamaged, last = fully damaged
    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject fixParticleSystem;
    [SerializeField] int initialHealth = 36;

    [Header("Visual Step Settings")]
    [SerializeField] int hitsPerVisualStep = 3;     // How many hits per visual change

    private SpriteRenderer visual;
    private int damageSteps;                          // visuals.Length - 1

    private int health;                               // main health value
    private int damageIndex = 0;                      // which sprite to show
    private int oldDamageIndex = 0;

    private Vector3 origHealthBarSize;

    private void Start()
    {
        visual = GetComponent<SpriteRenderer>();
        origHealthBarSize = healthBar.transform.localScale;
        damageSteps = visuals.Length - 1;

        health = initialHealth;
        UpdateDamageIndexFromHealth();
        UpdateVisuals();
        CalculateHealthBarLook();
    }

    // -----------------------------------------
    // DAMAGE
    // -----------------------------------------
    public void Damage()
    {
        health = Mathf.Max(0, health - 1);
        OnHealthChanged();
    }

    public void Damage(int dmg)
    {
        health = Mathf.Max(0, health - dmg);
        OnHealthChanged();
    }

    // -----------------------------------------
    // HEAL / FIX DAMAGE
    // -----------------------------------------
    public void FixDamage(int amount)
    {
        health = Mathf.Min(initialHealth, health + amount);
        OnHealthChanged();

        // Sparkle effect when healing
        fixParticleSystem.GetComponent<ParticleSystem>()?.Play();

        // Notify system
        var udp = GameObject.FindGameObjectWithTag("LevelUDPManager");
        udp?.GetComponent<LevelUDPCommunicator>().SendMessage("fixPainting");
    }

    // Full restore (call this when player has 12 fix items)
    public void FullRestore()
    {
        health = initialHealth;
        damageIndex = 0;
        oldDamageIndex = 0;

        visual.sprite = visuals[0];
        healthBar.transform.localScale = origHealthBarSize;

        fixParticleSystem.GetComponent<ParticleSystem>()?.Play();

        var udp = GameObject.FindGameObjectWithTag("LevelUDPManager");
        udp?.GetComponent<LevelUDPCommunicator>().SendMessage("fixPainting");
    }

    // -----------------------------------------
    // INTERNAL HEALTH HANDLING
    // -----------------------------------------
    private void OnHealthChanged()
    {
        UpdateDamageIndexFromHealth();
        UpdateVisuals();
        CalculateHealthBarLook();
    }

    private void UpdateDamageIndexFromHealth()
    {
        int damageTaken = initialHealth - health;

        // Each visual step occurs after 'hitsPerVisualStep' hits
        damageIndex = damageTaken / hitsPerVisualStep;

        damageIndex = Mathf.Clamp(damageIndex, 0, damageSteps);
    }

    // -----------------------------------------
    // VISUALS + HEALTH BAR
    // -----------------------------------------
    private void UpdateVisuals()
    {
        var udp = GameObject.FindGameObjectWithTag("LevelUDPManager");

        if (damageIndex > oldDamageIndex)
        {
            // Damage occurred
            visual.sprite = visuals[damageIndex];

            CinemachineImpulseSource cm = GetComponent<CinemachineImpulseSource>();
            cm?.GenerateImpulseWithForce(0.2f);

            udp?.GetComponent<LevelUDPCommunicator>().SendMessage("damagePainting");
        }
        else if (damageIndex < oldDamageIndex)
        {
            // Healing occurred
            visual.sprite = visuals[damageIndex];
            fixParticleSystem.GetComponent<ParticleSystem>()?.Play();

            udp?.GetComponent<LevelUDPCommunicator>().SendMessage("fixPainting");
        }

        oldDamageIndex = damageIndex;
    }

    private void CalculateHealthBarLook()
    {
        float healthPercent = (float)health / initialHealth;

        healthBar.transform.localScale = new Vector3(
            origHealthBarSize.x * healthPercent,
            origHealthBarSize.y,
            origHealthBarSize.z
        );
    }

    public float GetAmountOfDamage()
    {
        return 1-((float)health / initialHealth);
    }

    // -----------------------------------------
    // GETTERS
    // -----------------------------------------
    public int GetHealth() => health;
    public float GetDamagePercent() => 1f - (float)health / initialHealth;
}