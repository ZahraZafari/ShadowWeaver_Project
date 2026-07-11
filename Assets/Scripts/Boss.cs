using UnityEngine;

public class Boss : MonoBehaviour
{
    public int health = 5;
    public float speed = 1f;
    public float attackCooldown = 1.5f;
    public float detectionRange = 8f;
    public Transform pointA;
    public Transform pointB;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public GameObject winPanel;
    public GameObject finishLine;
    
    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip deathClip;

    private Transform target;
    private SpriteRenderer sr;
    private float attackTimer = 0f;
    private bool isPlayerNear = false;
    private bool isDead = false;

    void Start()
    {
        target = pointA;
        sr = GetComponent<SpriteRenderer>();
        
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.loop = false;
        }
        
        if (finishLine != null)
            finishLine.SetActive(false);
        if (winPanel != null)
            winPanel.SetActive(false);
    }

    void Update()
    {
        if (isDead) return;

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            target = (target == pointA) ? pointB : pointA;
        }

        GameObject aria = GameObject.Find("Aria");
        GameObject shadow = GameObject.Find("Shadow");

        if (aria != null && Vector3.Distance(transform.position, aria.transform.position) < detectionRange)
        {
            isPlayerNear = true;
        }
        else if (shadow != null && Vector3.Distance(transform.position, shadow.transform.position) < detectionRange)
        {
            isPlayerNear = true;
        }
        else
        {
            isPlayerNear = false;
        }

        if (isPlayerNear)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackCooldown)
            {
                attackTimer = 0f;
                ThrowProjectile();
            }
        }
    }

    void ThrowProjectile()
    {
        if (projectilePrefab == null || firePoint == null) return;

        GameObject targetChar = GameObject.Find("Shadow");
        if (targetChar == null) return;

        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector2 direction = (targetChar.transform.position - firePoint.position).normalized;
            rb.velocity = direction * 10f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        if (collision.gameObject.name == "Shadow" || collision.gameObject.name == "Aria")
        {
            TakeDamage(1);
            
            if (!isDead)
            {
                PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
                if (health != null)
                    health.TakeDamage(1);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        health--;
        sr.color = Color.white;
        Invoke("ResetColor", 0.2f);

        if (health <= 0)
        {
            Die();
        }
    }

    void ResetColor()
    {
        sr.color = Color.red;
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        PlayDeathSound();

        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("BossProjectile");
        foreach (GameObject proj in projectiles)
        {
            Destroy(proj);
        }

        if (finishLine != null)
            finishLine.SetActive(true);
        if (winPanel != null)
            winPanel.SetActive(true);

        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;

        Destroy(gameObject, deathClip != null ? deathClip.length : 0.1f);
    }

    void PlayDeathSound()
    {
        if (deathClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathClip);
        }
    }
}