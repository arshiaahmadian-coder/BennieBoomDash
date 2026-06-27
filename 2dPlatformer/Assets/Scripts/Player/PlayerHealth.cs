using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private Slider healthbar;

    [SerializeField] private AudioClip healupClip;
    [SerializeField] private AudioSource audioSource;

    private float minPitch = 0.9f;
    private float maxPitch = 1.1f;

    private int currentHealth;
    private ScreenShake screenShake;

    public static PlayerHealth instance;
    private void Awake() { instance = this; }

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
        screenShake = FindFirstObjectByType<ScreenShake>();
    }

    private void UpdateUI()
    {
        healthbar.value = (currentHealth <= 100 && currentHealth >= 0) ? currentHealth : 0;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateUI();
        if (currentHealth <= 0)
        {
            Die();
        }

        screenShake.Shake(2f, 2f, 0.15f);
    }

    public void GetHealth(int healthAmount)
    {
        currentHealth += healthAmount;
        if (currentHealth > 100) currentHealth = 100;
        UpdateUI();

        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.PlayOneShot(healupClip);
    }

    public void Die()
    {
        GameManager.instance.Gameover(false);
    }
}
