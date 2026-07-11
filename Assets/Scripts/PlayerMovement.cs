using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpForce = 10f;
    
    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    
    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip footstepClip;
    public AudioClip jumpClip;
    public AudioClip hitClip;
    public float footstepInterval = 0.3f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float lastDirection = 1f;
    private float footstepTimer = 0f;
    private float hitCooldown = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.loop = false;
        }
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (moveInput != 0)
        {
            lastDirection = moveInput;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            PlayJumpSound();
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            ThrowProjectile();
        }

        HandleFootstepSound(moveInput);

        if (hitCooldown > 0)
        {
            hitCooldown -= Time.deltaTime;
        }
    }

    void HandleFootstepSound(float moveInput)
    {
        bool isMoving = (moveInput > 0.1f || moveInput < -0.1f) && isGrounded;

        if (isMoving)
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0f)
            {
                PlayFootstep();
                footstepTimer = footstepInterval;
            }
        }
        else
        {
            footstepTimer = 0f;
        }
    }

    void PlayFootstep()
    {
        if (footstepClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(footstepClip);
        }
    }

    void PlayJumpSound()
    {
        if (jumpClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(jumpClip);
        }
    }

    void PlayHitSound()
    {
        if (hitClip != null && audioSource != null && hitCooldown <= 0)
        {
            audioSource.PlayOneShot(hitClip);
            hitCooldown = 0.5f;
        }
    }

    void ThrowProjectile()
    {
        if (projectilePrefab == null || firePoint == null) return;

        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rbProj = proj.GetComponent<Rigidbody2D>();

        if (rbProj != null)
        {
            rbProj.velocity = new Vector2(lastDirection * projectileSpeed, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            isGrounded = true;
        }

        if (!collision.gameObject.CompareTag("Ground") && collision.gameObject.layer != LayerMask.NameToLayer("Default"))
        {
            PlayHitSound();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            isGrounded = false;
        }
    }
}