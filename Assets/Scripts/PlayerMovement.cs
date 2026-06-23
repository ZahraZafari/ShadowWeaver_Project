using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // حرکت چپ و راست
        float moveInput = Input.GetAxisRaw("Horizontal"); // GetAxisRaw حرکت را سریع‌تر و بدون لیز خوردن می‌کند
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // پرش با کلید فلش بالا (Up Arrow)
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }
    }

    // تشخیص دقیق‌تر برخورد با زمین
    private void OnCollisionStay2D(Collision2D collision)
    {
        // اگر با هر چیزی برخورد کردی که تگ Ground دارد یا لایه‌اش Default است
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            isGrounded = true;
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