using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//Script to handle the player's health
public class PlayerHealth : MonoBehaviour
{
    //Current player's health
    private float m_health;
    //Timer for interpolation of the health bar
    private float m_lerpTimer;
    //Variable for player's max health
    [Header("Health Bar")]
    public float MaxHealth = 100f;
    //Variable for the speed at which a health change is visualized
    public float ChipSpeed = 2f;
    //Reference to UI elements of the health bar
    [SerializeField]
    private Image FrontHealthBar;
    [SerializeField]
    private Image BackHealthBar;

    [Header("Damage Overlay")]
    //Reference to UI element of the overlay when receiving damage
    public Image DamageOverlay;
    //Variable for the duration of the overlay
    public float Duration;
    //Variable for the fade time of the overlay
    public float FadeSpeed;
    //Current time passed of the overlay active
    private float m_durationTimer;

    void Start() {
        m_health = MaxHealth;
        DamageOverlay.color = new(DamageOverlay.color.r, DamageOverlay.color.g, DamageOverlay.color.b, 0);
    }

    void Update() {
        m_health = Mathf.Clamp(m_health, 0, MaxHealth);
        UpdateHealthGUI();
        if (DamageOverlay.color.a > 0)
        {
            m_durationTimer += Time.deltaTime;
            if (m_durationTimer > Duration) {
                float alphaTemp = DamageOverlay.color.a;
                alphaTemp -= Time.deltaTime * FadeSpeed;
                DamageOverlay.color = new(DamageOverlay.color.r, DamageOverlay.color.g, DamageOverlay.color.b, alphaTemp);
            }
        }
    }

    //Method to detect changes in health and visualize them in the health bar, either increasing or decreasing it
    public void UpdateHealthGUI() {
        float frontFill = FrontHealthBar.fillAmount;
        float backFill = BackHealthBar.fillAmount;
        float healthFrac = m_health / MaxHealth;
        if (backFill > healthFrac) {
            FrontHealthBar.fillAmount = healthFrac;
            BackHealthBar.color = Color.red;
            m_lerpTimer += Time.deltaTime;
            float completePercent = m_lerpTimer / ChipSpeed;
            completePercent *= completePercent;
            BackHealthBar.fillAmount = Mathf.Lerp(backFill, healthFrac, completePercent);
        }
        if (frontFill < healthFrac)
        {
            BackHealthBar.fillAmount = healthFrac;
            BackHealthBar.color = Color.green;
            m_lerpTimer += Time.deltaTime;
            float completePercent = m_lerpTimer / ChipSpeed;
            completePercent *= completePercent;
            FrontHealthBar.fillAmount = Mathf.Lerp(frontFill, BackHealthBar.fillAmount, completePercent);
        }
    }

    //Method for receiving damage from any source
    public void TakeDamage(float damage)
    {
        m_health -= damage;
        if(m_health < 0) {
            m_health = 0f;
        }
        m_lerpTimer = 0f;
        DamageOverlay.color = new(DamageOverlay.color.r, DamageOverlay.color.g, DamageOverlay.color.b, 1);
        m_durationTimer = 0f;
    }

    //Method for restoring health from any source
    public void Cure(float amount) {
        m_health += amount;
        if(m_health < MaxHealth) {
            m_health = MaxHealth;
        }
        m_lerpTimer = 0f;
    }
}
