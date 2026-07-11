using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth = 5;
    public Text healthText;
    public GameObject gameOverPanel;
    
    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip gameOverClip;

    private bool isDead = false;

    void Start()
    {
        UpdateHealthUI();
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
        
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.loop = false;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        UpdateHealthUI();

        if (currentHealth == 0)
        {
            isDead = true;
            GameOver();
        }
    }

    public void AddHealth(int amount)
    {
        if (isDead) return;

        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        UpdateHealthUI();
        Debug.Log("Health added! Current: " + currentHealth);
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
            healthText.text = "❤️ x " + currentHealth;
    }

    void GameOver()
    {
        PlayGameOverSound();
        
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Debug.Log("GameOverPanel is now ACTIVE!");
        }
        else
        {
            Debug.Log("GameOverPanel is NULL!");
        }
        Time.timeScale = 0f;
    }

    void PlayGameOverSound()
    {
        if (gameOverClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(gameOverClip);
        }
    }

    public void RestartLevel()
    {
        isDead = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}