using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public int damage = 1;

    void Start()
    {
        Destroy(gameObject, 3f); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Boss")
        {
            Boss boss = other.GetComponent<Boss>();
            if (boss != null)
            {
                boss.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Platform")
        {
            Destroy(gameObject);
        }
    }
}