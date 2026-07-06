using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Shadow" || other.gameObject.name == "Aria")
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Platform")
        {
            Destroy(gameObject);
        }
    }
}