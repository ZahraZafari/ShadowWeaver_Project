using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth = 5;
    public Text healthText;
    public GameObject gameOverPanel;
    private bool isDead = false;

    void Start()
    {
        // currentHealth = maxHealth; // این خط رو کامنت کن
        UpdateHealthUI();
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
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

    public void RestartLevel()
    {
        isDead = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}