using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered by: " + other.gameObject.name);

        if (other.gameObject.name == "Shadow" || other.gameObject.name == "Aria")
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.AddHealth(healAmount);
                Debug.Log("Health added! New health: " + health.currentHealth);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("PlayerHealth component not found on: " + other.gameObject.name);
            }
        }
    }
}